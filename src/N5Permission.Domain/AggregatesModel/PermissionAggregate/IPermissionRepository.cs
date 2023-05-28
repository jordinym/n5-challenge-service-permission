using N5Permission.Domain.SeedWork;

namespace N5Permission.Domain.AggregatesModel.PermissionAggregate
{
    public interface IPermissionRepository : IRepository<PermissionEntity>
    {
        PermissionEntity Add(PermissionEntity permission);
        PermissionEntity Update(PermissionEntity permission);
        Task<PermissionEntity> GetPermissionById(int id);
        Task<List<PermissionEntity>> GetPermissions();
    }
}
