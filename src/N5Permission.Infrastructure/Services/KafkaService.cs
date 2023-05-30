using Confluent.Kafka;
using Microsoft.Extensions.Options;
using N5Permission.Application.Common.Services;
using N5Permission.Domain.Common;
using System.Text.Json;

namespace N5Permission.Infrastructure.Services
{
    public class KafkaService : IKafkaService
    {
        private readonly ProducerConfig _config;

        public KafkaService(IOptions<ProducerConfig> config)
        {
            _config = config.Value;
        } 

        public async Task PublishEventAsync(string topic, BaseEvent baseEvent)
        {
            using var producer = new ProducerBuilder<string, string>(_config)
               .SetKeySerializer(Serializers.Utf8)
               .SetValueSerializer(Serializers.Utf8)
               .Build();

            var eventMessage = new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonSerializer.Serialize(baseEvent, baseEvent.GetType())
            };

            var deliveryResult = await producer.ProduceAsync(topic, eventMessage);

            if (deliveryResult.Status == PersistenceStatus.NotPersisted)
            {
                throw new Exception($"Could not produce {baseEvent.GetType().Name} message to topic - {topic} due to the following reason: {deliveryResult.Message}.");
            }
        }
    }
}
