using HospitalQueueMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalQueueMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Token> Tokens { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Token>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Tokens)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Doctor", NormalizedName = "DOCTOR" },
                new IdentityRole { Id = "3", Name = "Reception", NormalizedName = "RECEPTION" }
            );

            // Seed Users
            var hasher = new PasswordHasher<IdentityUser>();

            var admin = new IdentityUser { Id = "10", UserName = "admin", NormalizedUserName = "ADMIN" };
            admin.PasswordHash = hasher.HashPassword(admin, "123");

            var doctor = new IdentityUser { Id = "11", UserName = "doctor", NormalizedUserName = "DOCTOR" };
            doctor.PasswordHash = hasher.HashPassword(doctor, "123");

            var reception = new IdentityUser { Id = "12", UserName = "reception", NormalizedUserName = "RECEPTION" };
            reception.PasswordHash = hasher.HashPassword(reception, "123");

            builder.Entity<IdentityUser>().HasData(admin, doctor, reception);

            // Assign Roles to Users
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "10", RoleId = "1" }, // admin -> Admin
                new IdentityUserRole<string> { UserId = "11", RoleId = "2" }, // doctor -> Doctor
                new IdentityUserRole<string> { UserId = "12", RoleId = "3" }  // reception -> Reception
            );
        }
    }
}
