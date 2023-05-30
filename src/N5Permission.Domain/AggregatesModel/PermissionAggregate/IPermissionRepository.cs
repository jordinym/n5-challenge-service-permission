using N5Permission.Domain.SeedWork;

namespace N5Permission.Domain.AggregatesModel.PermissionAggregate
{
    public interface IPermissionRepository : IRepository<PermissionEntity>
    {  
        Task<List<PermissionEntity>> GetPermissions();
    }
}
