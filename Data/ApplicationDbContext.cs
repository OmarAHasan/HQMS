using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HospitalQueueMS.Models;

namespace HospitalQueueMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Token>().HasIndex(t => new { t.ClinicId, t.TokenId });
            modelBuilder.Entity<Token>().HasIndex(t => t.Status);

            base.OnModelCreating(modelBuilder);
        }
    }
}
