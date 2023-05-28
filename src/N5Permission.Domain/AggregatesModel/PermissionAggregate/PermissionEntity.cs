using N5Permission.Domain.Common;

namespace N5Permission.Domain.AggregatesModel.PermissionAggregate
{
    public class PermissionEntity : BaseEntity
    {
        public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermisionTypeId { get; set; }
        public virtual PermissionTypeEntity PermissionType { get; set; }

    }
}
