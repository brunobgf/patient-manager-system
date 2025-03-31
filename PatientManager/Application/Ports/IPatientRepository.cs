using PatientManager.Domain;

namespace PatientManager.Application.Ports
{
    public interface IPatientRepository
    {
        void Add(Patient patient);
        Patient? GetById(int id);
    }
}