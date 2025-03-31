namespace PatientManager.Application.Ports
{
    public interface IRabbitMqService
    {
        Task SendMessage(string message);
    }
}