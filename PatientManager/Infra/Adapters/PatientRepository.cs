using PatientManager.Domain.Model;
using PatientManager.Domain.Ports;
using PatientManager.Infra.Persistance;

namespace PatientManager.Infra.Adapters
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public Patient? GetById(int id)
        {
            return _context.Patients.Find(id);
        }
    }
}
