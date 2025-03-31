using System.Text;
using System.Threading.Channels;
using PatientManager.Application.Ports;
using RabbitMQ.Client;

namespace PatientManager.Infra.Messaging
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnectionFactory _connectionFactory;

        public RabbitMqService(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task SendMessage(string message)
        {
            var connection =  await _connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: "patient_notifications", durable: false, exclusive: false, autoDelete: false, arguments: null).ConfigureAwait(false);

            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: "", routingKey: "patient_notifications", body: body);
        }
    }
}
