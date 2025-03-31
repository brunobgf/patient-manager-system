using System.Text;
using RabbitMQ.Client;
using System.Threading.Tasks;
using PatientManager.Application.Ports;

namespace PatientManager.Infra.Messaging
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnectionFactory _factory;

        public RabbitMqService(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task SendMessage(string message)
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: "patient_notifications", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);

      
           await channel.BasicPublishAsync(exchange: "", routingKey: "patient_notifications", body: body);
         
        }
    }
}