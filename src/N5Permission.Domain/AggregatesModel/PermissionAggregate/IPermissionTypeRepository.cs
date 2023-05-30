using N5Permission.Domain.SeedWork;

namespace N5Permission.Domain.AggregatesModel.PermissionAggregate
{
    public interface IPermissionTypeRepository : IRepository<PermissionTypeEntity>
    {
        PermissionTypeEntity Add(PermissionTypeEntity permissionType);
        PermissionTypeEntity Update(PermissionTypeEntity permissionType);
        Task<PermissionTypeEntity> GetPermissionById(int id);
        Task<List<PermissionTypeEntity>> GetPermissionTypes();
    }
}
