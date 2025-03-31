using Moq;
using PatientManager.Domain.Model;
using PatientManager.Domain.Ports;
using PatientManager.Domain.Services;

namespace PatientManagerTest
{

    public class PatientUseCasesTests
    {
        [Fact]
        public void RegisterPatient_ShouldSendNotification()
        {
            var mockRepo = new Mock<IPatientRepository>();

            var mockRabbitMq = new Mock<IRabbitMqService>();

            var service = new PatientUseCase(mockRepo.Object, mockRabbitMq.Object);
            var patient = new Patient { Name = "John Doe", Age = 30, Weight = 80, Height = 1.80 };

            service.RegisterPatient(patient);

            mockRepo.Verify(r => r.Add(It.IsAny<Patient>()), Times.Once);
            mockRabbitMq.Verify(m => m.SendMessage(It.IsAny<string>()), Times.Once);


        }

        [Fact]
        public void RegisterPatient_ShouldNotSendNotification_WhenRepositoryFails()
        {
            var mockRepo = new Mock<IPatientRepository>();
            var mockRabbitMq = new Mock<IRabbitMqService>();
            var service = new PatientUseCase(mockRepo.Object, mockRabbitMq.Object);
            var patient = new Patient { Name = "John Doe", Age = 30, Weight = 80, Height = 1.80 };

            mockRepo.Setup(r => r.Add(It.IsAny<Patient>())).Throws(new Exception("Repository error"));

            Assert.Throws<Exception>(() => service.RegisterPatient(patient));
            mockRabbitMq.Verify(m => m.SendMessage(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void GetPatient_ShouldReturnPatient_WhenPatientExists()
        {
            var mockRepo = new Mock<IPatientRepository>();
            var mockRabbitMq = new Mock<IRabbitMqService>();
            var service = new PatientUseCase(mockRepo.Object, mockRabbitMq.Object);
            var patient = new Patient { Name = "John Doe", Age = 30, Weight = 80, Height = 1.80 };

            mockRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(patient);

            var result = service.GetPatient(1);

            Assert.Equal(patient, result);
        }

        [Fact]
        public void GetPatient_ShouldReturnNull_WhenPatientDoesNotExist()
        {
            var mockRepo = new Mock<IPatientRepository>();
            var mockRabbitMq = new Mock<IRabbitMqService>();
            var service = new PatientUseCase(mockRepo.Object, mockRabbitMq.Object);

            mockRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns(value: null as Patient);

            var result = service.GetPatient(1);

            Assert.Null(result);
        }
    }
}
