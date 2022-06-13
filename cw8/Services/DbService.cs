using cw8.Models;
using cw8.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace cw8.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _dbContext;
        public DbService(MainDbContext dbContext)
        {
            _dbContext = dbContext;    
        }

        public async Task<SomeSortOfDoctor> GetDoctor(int id)
        {
            return await _dbContext.Doctor.Where(e => e.IdDoctor == id)
            .Select(x => new SomeSortOfDoctor
              {
                  FirstName = x.FirstName,
                  LastName = x.LastName,
                  Email = x.Email
              }).FirstOrDefaultAsync();
        }
        public async Task AddDoctor(SomeSortOfDoctor doctor)
        {
            _dbContext.Add(doctor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateDoctor(SomeSortOfDoctor doctorDTO, int id)
        {
            Doctor doctor = _dbContext.Doctor.Where(e => e.IdDoctor == id).FirstOrDefault();
            if (doctor == null)
            {
                throw new Exception($"Nie znaleziono doktora o ID {id}");
            }
            doctor.FirstName = doctorDTO.FirstName;
            doctor.LastName = doctorDTO.LastName;
            doctor.Email = doctorDTO.Email;
            _dbContext.Attach(doctor);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveDoctor(int id)
        {
            Doctor doctor = _dbContext.Doctor.Where(e => e.IdDoctor == id).FirstOrDefault();
            if (doctor == null)
            {
                throw new Exception($"Nie znaleziono doktora o ID {id}");
            }
            _dbContext.Remove(doctor);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<SomeSortOfPrescription> GetPrescription(int id)
        {
            SomeSortOfPrescription prescription = _dbContext.Prescription.Where(e => e.IdPrescription == id)
                .Select(e => new SomeSortOfPrescription
                {
                    Date = e.Date,
                    DueDate = e.DueDate,
                    Patient = new SomeSortOfPatient { 
                        FirstName = e.Patient.FirstName, 
                        LastName = e.Patient.LastName,
                        Birthdate = e.Patient.Birthdate
                      },
                    Doctor = new SomeSortOfDoctor { 
                        FirstName = e.Doctor.FirstName, 
                        LastName = e.Doctor.LastName, 
                        Email = e.Doctor.Email },
                    Medicaments = e.Prescription_Medicaments.Select(p => new SomeSortOfMedicament
                    {
                        Name = p.Medicament.Name,
                        Description = p.Medicament.Description,
                        Type = p.Medicament.Type
                    })
                }).FirstOrDefault();
            if (prescription == null)
            {
                throw new Exception($"Recepta o ID {id} nie istnieje");
            }
            return prescription;
        }
    }
}
