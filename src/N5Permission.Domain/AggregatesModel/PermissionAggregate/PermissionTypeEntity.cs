using N5Permission.Domain.Common;

namespace N5Permission.Domain.AggregatesModel.PermissionAggregate
{
    public class PermissionTypeEntity : BaseEntity
    {
        //public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PermissionEntity> Permissions { get; set; }
        public PermissionTypeEntity()
        {
            Permissions = new HashSet<PermissionEntity>();
        }
    }
}
