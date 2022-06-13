using Microsoft.EntityFrameworkCore;

namespace cw8.Models
{
    public class MainDbContext : DbContext
    {
        protected MainDbContext()
        {
        }
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicament { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Patient>(p =>
            {
                p.HasKey(e => e.IdPatient);
                p.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                p.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                p.Property(e => e.Birthdate).IsRequired();

                p.HasData(
                    new Patient { IdPatient = 1, FirstName = "Bob", LastName = "Doe", Birthdate = DateTime.Now },
                    new Patient { IdPatient = 2, FirstName = "Jan", LastName = "Kowal", Birthdate = DateTime.Parse("2022-01-10") }
                    );
            });

            modelBuilder.Entity<Doctor>(d =>
            {
                d.HasKey(e => e.IdDoctor);
                d.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                d.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                d.Property(e => e.Email).IsRequired().HasMaxLength(100);

                d.HasData(
                   new Doctor { IdDoctor = 1, FirstName = "Doctor", LastName = "Jack", Email = "123@gmail.com" },
                   new Doctor { IdDoctor = 2, FirstName = "Profesor", LastName = "X", Email = "mail@mail.com" }
                   );
            });

            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasKey(e => e.IdPrescription);
                p.Property(e => e.Date).IsRequired();
                p.Property(e => e.DueDate).IsRequired();
                p.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient);
                p.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor);

                p.HasData(
                    new Prescription { IdPrescription = 1, Date = DateTime.Parse("2022-01-01"), DueDate = DateTime.Parse("2030-01-01"), IdPatient = 1, IdDoctor = 1 },
                    new Prescription { IdPrescription = 2, Date = DateTime.Parse("2022-05-05"), DueDate = DateTime.Parse("2030-05-05"), IdPatient = 2, IdDoctor = 2 }
                    );
            });

            modelBuilder.Entity<Prescription_Medicament>(p =>
            {
     
                p.HasKey(e => new { e.IdMedicament, e.IdPrescription});
                p.Property(e => e.Dose).IsRequired();
                p.Property(e => e.Details).IsRequired().HasMaxLength(100);
                p.HasOne(e => e.Medicament).WithMany(e => e.Prescription_Medicaments).HasForeignKey(e => e.IdMedicament);
                p.HasOne(e => e.Prescription).WithMany(e => e.Prescription_Medicaments).HasForeignKey(e => e.IdPrescription);

                p.HasData(
                  new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 5, Details = "na czczo" },
                  new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 1, Details = "przy jedzeniu" }
                  );
          
        });

            modelBuilder.Entity<Medicament>(m =>
            {
                m.HasKey(e => e.IdMedicament);
                m.Property(e => e.Name).IsRequired().HasMaxLength(100);
                m.Property(e => e.Description).IsRequired().HasMaxLength(100);
                m.Property(e => e.Type).IsRequired().HasMaxLength(100);

                m.HasData(
                   new Medicament { IdMedicament = 1, Name = "Apap", Description = "Na bol glowy", Type = "tabletka" },
                   new Medicament { IdMedicament = 2, Name = "Ibuprom", Description = "Na bol glowy", Type = "tabletka" });

            });





        }



    }
}
