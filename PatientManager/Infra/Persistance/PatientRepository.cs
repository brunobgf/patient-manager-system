using PatientManager.Application.Ports;
using PatientManager.Domain;
using PatientManager.Infraestructure.Persistance;

namespace PatientManager.Infra.Persistance
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
