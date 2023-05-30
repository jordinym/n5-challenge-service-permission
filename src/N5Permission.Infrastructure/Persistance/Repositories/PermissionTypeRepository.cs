using Microsoft.EntityFrameworkCore;
using N5Permission.Application.Common;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Infrastructure.Persistance.Repositories.Base;

namespace N5Permission.Infrastructure.Persistance.Repositories
{
    public class PermissionTypeRepository : BaseRepository<PermissionTypeEntity>, IPermissionTypeRepository
    {
        private readonly N5PermissionContext _context;  

        public PermissionTypeRepository(N5PermissionContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
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
