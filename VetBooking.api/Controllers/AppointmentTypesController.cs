using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;

namespace VetBooking.api.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentTypesController(IAppointmentTypeService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<AppointmentTypeResponse>>> GetAllAppointmentTypes(CancellationToken cancellationToken)
        {
            var appointmentTypes = await service.GetAllAsync(cancellationToken);

            if (appointmentTypes is null || appointmentTypes.Count is 0)
            {
                return NotFound("No appointment types found.");
            }

            return Ok(appointmentTypes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentTypeResponse>> GetAllAppointmentTypesById(int id, CancellationToken cancellationToken)
        {
            var appointmentType = await service.GetByIdAsync(id, cancellationToken);

            if (appointmentType is null)
            {
                return NotFound($"Appointment type with ID {id} not found.");
            }
            return Ok(appointmentType);
        }


        [HttpPost]

        public async Task<ActionResult<AppointmentTypeResponse>> CreateAppointmentType(CreateAppointmentTypeRequest request, CancellationToken cancellationToken)
        {
            var createdAppointmentType = await service.CreateAsync(request, cancellationToken);

            return CreatedAtAction(nameof(GetAllAppointmentTypesById), new { id = createdAppointmentType.Id }, createdAppointmentType);

        }



        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAppointmentType(int id, CancellationToken cancellationToken)
        {
            var appointmentType = await service.GetByIdAsync(id, cancellationToken);


            if (appointmentType is null)
            {
                return NotFound("Appointment Type Not Found.");
            }


            var deleted = await service.DeleteAsync(id, cancellationToken);


            if (!deleted)
            {
                return BadRequest("Failed to delete Appointment Type.");
            }

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointmentType(int id, UpdateAppointmentTypeRequest request, CancellationToken cancellationToken)
        {
            var updated = await service.UpdateAsync(id, request, cancellationToken);

            if (!updated)
            {
                return NotFound("Appointment Type Not Found.");
            }

            return NoContent();
        }

    }
