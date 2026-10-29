using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;

namespace VetBooking.api.Controllers;

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

            if (appointmentType is null)
            {
                return NotFound($"Appointment type with ID {id} not found.");
            }
            return Ok(appointmentType);
        }


        [HttpPost]

        public async Task<ActionResult<AppointmentTypeResponse>> CreateAppointmentType(CreateAppointmentTypeRequest request)
        {
            var createdAppointmentType = await service.CreateAsync(request);

            return CreatedAtAction(nameof(GetAllAppointmentTypesById), new { id = createdAppointmentType.Id }, createdAppointmentType);

        }



        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAppointmentType(int id)
        {
            var appointmentType = await service.GetByIdAsync(id);


            if (appointmentType is null)
            {
                return NotFound("Appointment Type Not Found.");
            }


            var deleted = await service.DeleteAsync(id);


            if (!deleted)
            {
                return BadRequest("Failed to delete Appointment Type.");
            }

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointmentType(int id, UpdateAppointmentTypeRequest request)
        {
            var updated = await service.UpdateAsync(id, request);

            if (!updated)
            {
                return NotFound("Appointment Type Not Found.");
            }

            return NoContent();
        }

    }
