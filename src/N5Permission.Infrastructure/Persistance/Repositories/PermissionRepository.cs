using Microsoft.EntityFrameworkCore;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Domain.SeedWork;
using System;

namespace N5Permission.Infrastructure.Persistance.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly N5PermissionContext _context; 

        public IUnitOfWork UnitOfWork => _context;

        public PermissionRepository(N5PermissionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public PermissionEntity Add(PermissionEntity permission)
        {
            return _context.Permissions
                .Add(permission)
                .Entity;
        }

        public Task<List<PermissionEntity>> GetPermissions()
        {
            return _context.Permissions.AsNoTracking().ToListAsync();
        }

        public PermissionEntity Update(PermissionEntity permission)
        {
            return _context.Permissions
                .Update(permission)
                .Entity;
        }

        public Task<PermissionEntity> GetPermissionById(int id)
        {
            return _context.Permissions.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
