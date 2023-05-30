using N5Permission.Domain.SeedWork;

namespace N5Permission.Domain.AggregatesModel.PermissionAggregate
{
    public interface IPermissionTypeRepository : IRepository<PermissionTypeEntity>
    { 
        Task<List<PermissionTypeEntity>> GetPermissionTypes();
    }
}
