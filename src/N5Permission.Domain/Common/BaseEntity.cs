using N5Permission.Domain.SeedWork;

namespace N5Permission.Domain.Common
{
    public class BaseEntity: EntityEvent
    {
        public string CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModificationUser { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool Status { get; set; }
    }
}
