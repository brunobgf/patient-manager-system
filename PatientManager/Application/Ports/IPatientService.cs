using PatientManager.Domain;

namespace PatientManager.Application.Ports
{
    public interface IPatientService
    {
        void RegisterPatient(Patient patient);
        Patient? GetPatient(int id);
    }
}