using PatientManager.Domain.Model;

namespace PatientManager.Domain.Ports
{
    public interface IPatientUseCase
    {
        void RegisterPatient(Patient patient);
        Patient? GetPatient(int id);
    }
}