using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumption.Api.BackgroundServices
{
    public class QueueListenerService : IHostedService
    {
        private readonly IModel _channel;
        private const string QueueName = "ReadingEvents";
        private EventingBasicConsumer? _consumer;

        public QueueListenerService(IModel channel)
        {
            _channel = channel;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _channel.QueueDeclare(QueueName, false, false, false, null);
            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += OnReceivedMessage;
            _channel.BasicConsume(QueueName, true, _consumer);

            return Task.CompletedTask;
        }

        private void OnReceivedMessage(object? model, BasicDeliverEventArgs eventArgs)
        {
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            System.Console.WriteLine($"Received message: {message}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_consumer != null)
            {
                _consumer.Received -= OnReceivedMessage;
            }
            return Task.CompletedTask;
        }
    }
}