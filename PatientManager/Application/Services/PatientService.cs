using PatientManager;
using PatientManager.Application.Ports;
using PatientManager.Domain;

namespace PatientManager.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IRabbitMqService _rabbitMqService;

        public PatientService(IPatientRepository repository, IRabbitMqService rabbitMqService)
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