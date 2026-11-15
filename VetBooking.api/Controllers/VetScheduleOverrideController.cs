using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;

namespace VetBooking.api.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class VetScheduleOverrideController(IVetScheduleService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<VetScheduleOverrideResponse>>> GetAllVetSchedule(int vetId  ,CancellationToken cancellationToken)
        {

            var schedule = await service.GetAllOverridesAsync(vetId, cancellationToken);

            if (schedule is null || schedule.Count is 0)
            {
                return NotFound("No Vet Schedule found.");
            }

            return Ok(schedule);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<VetScheduleOverrideResponse>> GetOverrideById(int vetId, int id, CancellationToken cancellationToken)
        {
            var schedule = await service.GetOverrideByIdAsync(id, cancellationToken);

            if (schedule is null)
            {
                return BadRequest($"Schedule override with ID {id} was not found.");
            }

            if (schedule.VetId != vetId)
            {
                return BadRequest("The requested schedule override does not belong to this vet.");
            }

            return Ok(schedule);
        }


        [HttpPost]
        public async Task<ActionResult<VetScheduleOverrideResponse>> CreateOverride(CreateVetScheduleOverrideRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return BadRequest("Vet schedule request cannot be null.");
            }
            
            try
            {
                var createdSchedule = await service.CreateOverrideAsync(request, cancellationToken);
                return CreatedAtAction(nameof(GetOverrideById), new { id = createdSchedule.Id }, createdSchedule);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVetSchedule(int id, CancellationToken cancellationToken)
        {
            var schedule = await service.GetOverrideByIdAsync(id, cancellationToken);

            if (schedule is null)
            {
                return NotFound("Schedule not found.");
            }

            var deleted = await service.DeleteOverrideAsync(id, cancellationToken);

            if (!deleted)
            {
                return BadRequest("Schedule could not be deleted");
            }

            return NoContent();
        }
    }

