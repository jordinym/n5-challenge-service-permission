using N5Permission.Domain.Common;
using N5Permission.Domain.SeedWork;

namespace N5Permission.Domain.AggregatesModel.PermissionAggregate
{
    public class PermissionTypeEntity : BaseEntity, IAggregateRoot
    {
        //public int Id { get; set; }
        public string Description { get; set; }
        public string RegistrationDate { get; set; }
        public virtual ICollection<PermissionEntity> Permissions { get; set; }
        public PermissionTypeEntity()
        {
            Permissions = new HashSet<PermissionEntity>();
        }
    }
}
