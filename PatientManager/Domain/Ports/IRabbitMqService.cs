namespace PatientManager.Domain.Ports
{
    public interface IRabbitMqService
    {
        Task SendMessage(string message);
    }
}