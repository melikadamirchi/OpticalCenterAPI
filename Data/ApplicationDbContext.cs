using Microsoft.EntityFrameworkCore;
using OpticalCenterAPI.Models;

namespace OpticalCenterAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<PatientDoctor> PatientDoctors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientDoctor>()
                .HasKey(pd => new { pd.PatientId, pd.DoctorId });


            modelBuilder.Entity<PatientDoctor>()
                .HasOne(pd => pd.Patient)
                .WithMany()
                .HasForeignKey(pd => pd.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PatientDoctor>()
                .HasOne(pd => pd.Doctor)
                .WithMany()
                .HasForeignKey(pd => pd.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}