using cw8.Models.DTO;
using cw8.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IDbService _dBservice;
        public PrescriptionController(IDbService dbservice)
        {
            _dBservice = dbservice;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> getPrescription(int id)
        {
            try
            {
                SomeSortOfPrescription prescription = await _dBservice.GetPrescription(id);
                return Ok(prescription);
            }
            catch (Exception e)
            {
                 return NotFound(e.Message);
            }

        }
    }
}
