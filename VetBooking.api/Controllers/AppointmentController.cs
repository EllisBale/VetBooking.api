using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;

namespace VetBooking.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController(IAppointmentService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AppointmentResponse>>> GetAllAppointments()
    {
        var appointments = await service.GetAllAsync();

        if (appointments is null || appointments.Count is 0)
        {
            return NotFound("No Appointments found.");
        }

        return Ok(appointments);
    }


}



