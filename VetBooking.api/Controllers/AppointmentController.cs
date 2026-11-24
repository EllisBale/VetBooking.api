using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;

namespace VetBooking.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController(IAppointmentService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AppointmentDetailResponse>>> GetAllAppointments(CancellationToken ct)
    {
        var app = await service.GetAllAsync(ct);

        if (app is null || app.Count is 0)
        {
            return NotFound("No Appointments found.");
        }

        return Ok(app);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentDetailResponse?>> GetAppointmentById(int id, CancellationToken ct)
    {
        var app = await service.GetByIdAsync(id, ct);

        if (app is null)
        {
            return NotFound("Appointment not found.");
        }

        return Ok(app);
    }

    [HttpPost]

    public async Task<ActionResult<AppointmentDetailResponse?>> CreateAppointment([FromBody] CreateAppointmentRequest req, CancellationToken ct)
    {

        if (req is null)
        {
            return BadRequest("Appointment request body cannot be null.");
        }


        try
        {
            var createdAppointment = await service.CreateAsync(req, ct);

            if (createdAppointment is null)
            {
                return NotFound($"Pet ID {req.PetId} does not exist.");
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

        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }

        
    }


    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteAppointment(int id, CancellationToken ct)
    {
        var app = await service.GetByIdAsync(id, ct);

        if (app is null)
        {
            return NotFound("Appointment Not Found.");
        }

        var deleted = await service.DeleteAsync(id, ct);

        if (!deleted)
        {
            return BadRequest("Appointment could not be deleted.");
        }

        return NoContent();
    }

}



