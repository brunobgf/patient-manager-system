using Microsoft.EntityFrameworkCore;
using PatientManager.Domain.Model;

namespace PatientManager.Infra.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Patient> Patients {  get; set; }
    }
}
