using AmazeCare.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AmazeCare.Contexts
{
    public class RequestTrackerContext : DbContext
    {
        public RequestTrackerContext(DbContextOptions<RequestTrackerContext> options) : base(options)
        {
        }

        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<MedicalRecords> MedicalRecords { get; set; }
        public DbSet<Prescriptions> Prescriptions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary Key for Appointments
            modelBuilder.Entity<Appointments>()
                .HasKey(a => a.AppointmentId);

            // Appointment -> Patient (Many-to-One)
            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Patients)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            // Appointment -> Doctor (Many-to-One)
            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Doctors)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            // MedicalRecord -> Appointment (One-to-One)
            modelBuilder.Entity<MedicalRecords>()
                .HasOne(m => m.Appointments)
                .WithOne(a => a.MedicalRecords)
                .HasForeignKey<MedicalRecords>(m => m.AppointmentId);

            // Prescription -> MedicalRecord (Many-to-One)
            modelBuilder.Entity<Prescriptions>()
                .HasOne(p => p.MedicalRecords)
                .WithMany()
                .HasForeignKey(p => p.RecordId);
        }
    }
}
