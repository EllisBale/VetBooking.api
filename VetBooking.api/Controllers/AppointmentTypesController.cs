using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;

namespace VetBooking.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentTypesController(IAppointmentTypeService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<AppointmentTypeResponse>>> GetAllAppointmentTypes()
        {
            var appointmentTypes = await service.GetAllAsync();

            if (appointmentTypes == null || appointmentTypes.Count == 0)
            {
                return NotFound("No appointment types found.");
            }

            return Ok(appointmentTypes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentTypeResponse>> GetAllAppointmentTypesById(int id)
        {
            var appointmentType = await service.GetByIdAsync(id);

            if (appointmentType == null)
            {
                return NotFound($"Appointment type with ID {id} not found.");
            }
            return Ok(appointmentType);
        }
        
    }
}
