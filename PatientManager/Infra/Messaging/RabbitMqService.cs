using System.Text;
using RabbitMQ.Client;
using PatientManager.Application.Ports;
using RabbitMQ.Client.Exceptions;
namespace PatientManager.Infra.Messaging
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnectionFactory _factory;

        public RabbitMqService(IConnectionFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Message cannot be null or empty.", nameof(message));

            try
            {
                await using var connection = await _factory.CreateConnectionAsync();
                await using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(
                    queue: "patient_notifications",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: "patient_notifications",
                    body: body
                );
            }
            catch (BrokerUnreachableException ex)
            {
                Console.WriteLine($"RabbitMQ connection error: {ex.Message}");
                throw;
            }
        }
    }
}