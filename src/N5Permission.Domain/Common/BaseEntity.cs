namespace N5Permission.Domain.Common
{
    public class BaseEntity
    {
        public string CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModificationUser { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool Status { get; set; }
    }
}
