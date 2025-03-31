using PatientManager.Domain.Model;
using PatientManager.Domain.Ports;

namespace PatientManager.Domain.Services
{
    public class PatientUseCase : IPatientUseCase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IRabbitMqService _rabbitMqService;

        public PatientUseCase(IPatientRepository repository, IRabbitMqService rabbitMqService)
        {
            _patientRepository = repository;
            _rabbitMqService = rabbitMqService;
        }

        public void RegisterPatient(Patient patient)
        {
            _patientRepository.Add(patient);
            _rabbitMqService.SendMessage($"Novo paciente cadastrado: {patient.Name}, IMC: {patient.BMI:F2}");

        }

        public Patient? GetPatient(int id)
        {
            return _patientRepository.GetById(id);
        }
    }
}