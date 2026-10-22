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

            return Ok(appointmentTypes);
        }
    }
}
