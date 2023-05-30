using Microsoft.EntityFrameworkCore;
using N5Permission.Application.Common;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Domain.SeedWork; 

namespace N5Permission.Infrastructure.Persistance.Repositories
{
    public class PermissionTypeRepository : IPermissionTypeRepository
    {
        private readonly N5PermissionContext _context; 

        public IUnitOfWork UnitOfWork => _context;

        public PermissionTypeRepository(N5PermissionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }  

        public PermissionTypeEntity Add(PermissionTypeEntity permissionType)
        {
            return _context.PermissionTypes
               .Add(permissionType)
               .Entity;
        }

        public PermissionTypeEntity Update(PermissionTypeEntity permissionType)
        {
            return _context.PermissionTypes
                .Update(permissionType)
                .Entity;
        }

        public Task<PermissionTypeEntity> GetPermissionById(int id)
        {
            return _context.PermissionTypes.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<PermissionTypeEntity>> GetPermissionTypes()
        {
            return _context.PermissionTypes.Select(r => new PermissionTypeEntity
            {
                Id = r.Id,
                Description = r.Description, 
                RegistrationDate = r.CreationDate.ToString(Constants.DateFormat.Date)
            })
                .AsNoTracking()
                .ToListAsync();
        }
         
    }
}
