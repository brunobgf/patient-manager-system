using Moq;
using PatientManager.Application.Ports;
using PatientManager.Application.Services;
using PatientManager.Domain;

namespace PatientManagerTest
{

    public class PatientServicesTests
    {
        [Fact]
        public void RegisterPatient_ShouldSendNotification()
        {
            var mockRepo = new Mock<IPatientRepository>();

            var mockRabbitMq = new Mock<IRabbitMqService>();

            var service = new PatientService(mockRepo.Object, mockRabbitMq.Object);
            var patient = new Patient { Name = "John Doe", Age = 30, Weight = 80, Height = 1.80 };

            service.RegisterPatient(patient);

            mockRepo.Verify(r => r.Add(It.IsAny<Patient>()), Times.Once);
            mockRabbitMq.Verify(m => m.SendMessage(It.IsAny<string>()), Times.Once);


        }
    }
}
