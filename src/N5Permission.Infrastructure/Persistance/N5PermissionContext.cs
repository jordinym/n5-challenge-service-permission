using MediatR;
using Microsoft.EntityFrameworkCore;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Domain.Common;
using N5Permission.Domain.SeedWork;
using N5Permission.Infrastructure.CrossCutting;
using N5Permission.Infrastructure.EntityConfigurations;

namespace N5Permission.Infrastructure.Persistance
{
    public class N5PermissionContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public N5PermissionContext(DbContextOptions<N5PermissionContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region DbSet
        public virtual DbSet<PermissionTypeEntity> PermissionTypes { get; set; }
        public virtual DbSet<PermissionEntity> Permissions { get; set; }
        #endregion

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PermissionConfiguration());
            builder.ApplyConfiguration(new PermissionTypeConfiguration());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();
            
            await _mediator.DispatchDomainEventsAsync(this);
            
            return await base.SaveChangesAsync(cancellationToken);
        }
        
        private void OnBeforeSaveChanges()
        {
            if (ChangeTracker.Entries().Any(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                var identity =  "SYSTEM";

                foreach (var entry in ChangeTracker.Entries()
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
    }
}