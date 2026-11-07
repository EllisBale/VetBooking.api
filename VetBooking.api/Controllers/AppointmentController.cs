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


    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentResponse?>> GetAppointmentById(int id)
    {
        var appointment = await service.GetByIdAsync(id);

        if (appointment is null)
        {
            return NotFound("Appointment not found.");
        }

        return Ok(appointment);
    }

    [HttpPost]

    public async Task<ActionResult<AppointmentResponse?>> CreateAppointment([FromBody] CreateAppointmentRequest request)
    {

        if (request is null)
        {
            return BadRequest("Appointment request body cannot be null.");
        }

        try
        {
            var createdAppointment = await service.CreateAsync(request);

            if (createdAppointment is null)
            {
                return NotFound($"Pet ID {request.PetId} does not exist.");
            }

            return CreatedAtAction(
                nameof(GetAppointmentById),
                new { id = createdAppointment?.Id },
                createdAppointment);
        }

        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

}



