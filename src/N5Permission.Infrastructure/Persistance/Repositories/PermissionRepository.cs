using Microsoft.EntityFrameworkCore;
using N5Permission.Application.Common;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Domain.SeedWork;
using N5Permission.Infrastructure.Persistance.Repositories.Base;
using System;

namespace N5Permission.Infrastructure.Persistance.Repositories
{
    public class PermissionRepository : BaseRepository<PermissionEntity>, IPermissionRepository
    {
        private readonly N5PermissionContext _context;  

        public PermissionRepository(N5PermissionContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
         

        public Task<List<PermissionEntity>> GetPermissions()
        {
            return _context.Permissions.Select(r => new PermissionEntity
            {
                Id = r.Id,
                EmployeeForename = r.EmployeeForename,
                EmployeeSurname = r.EmployeeSurname,
                RegistrationDate = r.CreationDate.ToString(Constants.DateFormat.Date),
                PermissionType = new PermissionTypeEntity
                {
                    Id = r.PermissionType.Id,
                    Description = r.PermissionType.Description,
                },
            })
                .AsNoTracking()
                .ToListAsync();
        } 
         
    }
}
