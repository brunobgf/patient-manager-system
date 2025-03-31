using Microsoft.EntityFrameworkCore;
using PatientManager.Domain;

namespace PatientManager.Infraestructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Patient> Patients {  get; set; }
    }
}
