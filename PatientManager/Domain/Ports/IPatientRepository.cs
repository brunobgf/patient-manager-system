using PatientManager.Domain.Model;

namespace PatientManager.Domain.Ports
{
    public interface IPatientRepository
    {
        void Add(Patient patient);
        Patient? GetById(int id);
    }
}