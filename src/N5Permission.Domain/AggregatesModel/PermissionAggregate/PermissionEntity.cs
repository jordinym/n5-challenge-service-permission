using N5Permission.Domain.Common;
using N5Permission.Domain.SeedWork;

namespace N5Permission.Domain.AggregatesModel.PermissionAggregate
{
    public class PermissionEntity : BaseEntity, IAggregateRoot
    {
        //public int Id { get; set; }
        public string EmployeeForename { get; set; }
        public string EmployeeSurname { get; set; }
        public int PermisionTypeId { get; set; }
        public string RegistrationDate { get; set; }
        public virtual PermissionTypeEntity PermissionType { get; set; }

    }
}
