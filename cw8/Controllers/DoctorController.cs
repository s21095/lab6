using cw8.Models;
using cw8.Models.DTO;
using cw8.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDbService _dBservice;
        public DoctorController(IDbService dbservice)
        {
            _dBservice = dbservice;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var doctor = await _dBservice.GetDoctor(id);

            if (doctor == null)
            {
                return NotFound();
            } else
            {
                return Ok(doctor);
            }
            
        }
        [HttpPost]
       
        public async Task<IActionResult> AddDoctor(SomeSortOfDoctor doctor)
        {
            Doctor doctorToAdd = new()
            {   
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };
            await _dBservice.AddDoctor(doctorToAdd);
            return Ok("Dodano doktora");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDoctor(SomeSortOfDoctor doctorDTO, int id)
        {
            try
            {
                await _dBservice.UpdateDoctor(doctorDTO, id);
                return Ok($"Edytowano doktora o ID {id}");
            }
            catch (Exception e)
            {
                    return NotFound(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveDoctor(int id)
        {
            try
            {
                await _dBservice.RemoveDoctor(id);
                return Ok($"Usunięto doktora o ID {id}");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
    
}
