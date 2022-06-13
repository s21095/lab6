using cw8.Models;
using cw8.Models.DTO;

namespace cw8.Services
{
    public interface IDbService
    {
        public Task<SomeSortOfDoctor> GetDoctor(int id);
        public Task AddDoctor(SomeSortOfDoctor doctorToAdd);
        public Task UpdateDoctor(SomeSortOfDoctor doctorDTO, int id);
        public Task RemoveDoctor(int id);
        public Task<SomeSortOfPrescription> GetPrescription(int id);
    }
}
