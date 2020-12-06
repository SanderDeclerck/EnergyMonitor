using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using BuildingConfiguration.Domain.Events;
using MediatR;
using NodaTime;
using RabbitMQ.Client;

namespace BuildingConfiguration.Infrastructure.EventHandlers
{
    public class ForwardDomainEventsToQueueEventHandler : INotificationHandler<MeterReadingRegisteredDomainEvent>
    {
        private readonly IModel _channel;
        private const string QueueName = "ReadingEvents";

        public ForwardDomainEventsToQueueEventHandler(IModel channel)
        {
            _channel = channel;
        }

        public Task Handle(MeterReadingRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            _channel.QueueDeclare(QueueName, false, false, false, null);
            var serialized = JsonSerializer.Serialize(notification, new JsonSerializerOptions
            {
                Converters = {
                    new InstantConverter()
                }
            });
            _channel.BasicPublish("", QueueName, null, Encoding.UTF8.GetBytes(serialized));

            return Task.CompletedTask;
        }

        public class InstantConverter : JsonConverter<Instant>
        {
            public override Instant Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
            {
                if (!reader.TryGetInt64(out var unixMilliseconds))
                {
                    return Instant.MinValue;
                }
                return Instant.FromUnixTimeMilliseconds(unixMilliseconds);
            }

            public override void Write(Utf8JsonWriter writer, Instant value, JsonSerializerOptions options)
            {
                writer.WriteNumberValue(value.ToUnixTimeMilliseconds());
            }
        }
    }
}