namespace N5Permission.Domain.Common
{
    public class BaseEvent
    {
        public string Id { get; set; }
        public string OperationName { get; set; }
        public string OperationFullName { get; set; }
    }
}
