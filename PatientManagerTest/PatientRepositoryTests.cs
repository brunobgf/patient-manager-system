using Microsoft.EntityFrameworkCore;
using PatientManager.Domain;
using PatientManager.Infra.Persistance;
using Assert = Xunit.Assert;

namespace PatientManagerTests
{
    public class PatientRepositoryTests
    {
        [Fact]
        public void AddPatient_ShouldStorePatient()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            using var context = new ApplicationDbContext(options);
            var repository = new PatientRepository(context);
            var patient = new Patient { Name = "Maria Cecília", Age = 25, Weight = 70, Height = 1.75 };

            repository.Add(patient);
            var retrievePatient = repository.GetById(patient.Id);

            Assert.NotNull(retrievePatient);

            Assert.Equal("Maria Cecília", retrievePatient.Name);

        }
    }
}
