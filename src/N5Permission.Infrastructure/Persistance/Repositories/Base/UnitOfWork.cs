using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using N5Permission.Domain.SeedWork;
using N5Permission.Application.Common;
using MediatR;
using N5Permission.Domain.Common;
using N5Permission.Infrastructure.CrossCutting;
using Microsoft.EntityFrameworkCore.Storage;

namespace N5Permission.Infrastructure.Persistance.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly N5PermissionContext _context; 
        private readonly AppSetting settings;
        private readonly IMediator _mediator;
        private Dictionary<Type, object> repositories;
        private bool _disposed;
        public Func<DateTime> CurrentDateTime { get; set; } = () => Constants.GetCustomDateTimeNow();
        private readonly IServiceProvider _serviceProvider;
        public static readonly ILoggerFactory DbCommandConsoleLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        public UnitOfWork(N5PermissionContext n5PermissionContext, IOptions<AppSetting> settings, IMediator mediator,
            IServiceProvider serviceProvider)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(IMediator));
            this.settings = settings.Value;
            _context = n5PermissionContext ?? throw new ArgumentNullException(nameof(N5PermissionContext));
            _serviceProvider = serviceProvider;
        }

        public DbContext Get()
        {
            return _context;
        }

        public T Repository<T>() where T : class
        {
            if (repositories == null)
                repositories = new Dictionary<Type, object>();

            var type = typeof(T);
            if (!repositories.ContainsKey(type))
            {
                repositories.Add(type, _serviceProvider.GetService(type));
            }

            return (T)repositories[type];
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();
            await _mediator.DispatchDomainEventsAsync(_context);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaveChanges()
        {
            if (_context.ChangeTracker.Entries().Any(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                var identity = "SYSTEM";

                foreach (var entry in _context.ChangeTracker.Entries()
                    .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified)))
                {
                    var baseEntity = entry.Entity as BaseEntity;

                    if (entry.State == EntityState.Added)
                    {
                        baseEntity.CreationDate = baseEntity.CreationDate == DateTime.MinValue ? DateTime.Now : baseEntity.CreationDate;
                        baseEntity.CreationUser = string.IsNullOrEmpty(baseEntity.CreationUser) ? identity : baseEntity.CreationUser;
                        baseEntity.ModificationDate = DateTime.Now;
                        baseEntity.ModificationUser = identity;
                        baseEntity.Status = true;
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        baseEntity.ModificationDate = DateTime.Now;
                        baseEntity.ModificationUser = identity;

                        entry.Property(nameof(baseEntity.CreationDate)).IsModified = false;
                        entry.Property(nameof(baseEntity.CreationUser)).IsModified = false;
                    }

                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                {
                    if (repositories != null)
                        repositories.Clear();

                    _context.Dispose();
                }

            _disposed = true;
        }
         
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _transaction.CommitAsync(cancellationToken);
            }
            finally
            {
                CloseTransaction();
            }
        }

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            return _transaction.RollbackAsync(cancellationToken);
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
