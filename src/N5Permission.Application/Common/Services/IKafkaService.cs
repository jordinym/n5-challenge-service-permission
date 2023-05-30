using N5Permission.Domain.Common;

namespace N5Permission.Application.Common.Services
{
    public interface IKafkaService
    {
        Task PublishEventAsync(string topic, BaseEvent baseEvent)  ;
    }
}
