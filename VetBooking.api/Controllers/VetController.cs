using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;

namespace VetBooking.api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VetController(IVetService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<VetResponse>>> GetAllVets(CancellationToken cancellationToken)
    {
        var vets = await service.GetAllAsync(cancellationToken);

        if (vets is null || vets.Count is 0)
        {
            return NotFound("No Vets found.");
        }

        return Ok(vets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VetResponse?>> GetById(int id, CancellationToken cancellationToken)
    {
        var vet = await service.GetByIdAsync(id, cancellationToken);

        if (vet is null)
        {
            return NotFound($"Vet with ID {id} not found.");
        }

        return Ok(vet);
    }


    [HttpPost]
    public async Task<ActionResult<VetResponse>> CreateVet(CreateVetRequest request, CancellationToken cancellationToken)
    {
        var createdVet = await service.CreateAsync(request, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = createdVet.Id }, createdVet);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteVet(int id, CancellationToken cancellationToken)
    {
        var vet = await service.GetByIdAsync(id, cancellationToken);

        if (vet is null)
        {
            return NotFound("Vet Not Found.");
        }

        var deleted = await service.DeleteAsync(id, cancellationToken);

        if (!deleted)
        {
            return BadRequest("Vet could not be deleted.");
        }

        return NoContent();
    }


    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateVet(int id, UpdateVetRequest request, CancellationToken cancellationToken)
    {
        var vet = await service.GetByIdAsync(id, cancellationToken);

        if (vet is null)
        {
            return NotFound("Vet Not Found.");
        }

        var updated = await service.UpdateAsync(id, request, cancellationToken);

        if (!updated)
        {
            return BadRequest("Vet could not be updated.");
        }

        return NoContent();
    }


    [HttpGet("{vetId}/working-hours")]
    public async Task<ActionResult<List<WorkingHoursResponse>>> GetWorkingHours(int vetId, CancellationToken cancellationToken)
    {

            var vetIdExist = await service.ExistsAsync(vetId);

            if (!vetIdExist)
            {
                return NotFound($"Vet with ID {vetId} not found.");
            }
       
            var workingHours = await service.GetWorkingHoursAsync(vetId, cancellationToken);

            if (workingHours is null)
            {
                return NotFound($"No working hours found for Vet with ID {vetId}.");
            }

            if (workingHours.Count == 0)
            {
                return NotFound($"No working hours found for Vet with ID {vetId}.");
            }

            return Ok(workingHours);
       

        
    }


    [HttpPut("{id}/working-hours/{day}")]
    public async Task<IActionResult> SetWorkingHours(int id, DayOfWeek day, SetWorkingHoursRequest request, CancellationToken cancellationToken)
    {
        if (request.StartTime >= request.EndTime)
        {
            return BadRequest("Start time must be before end time.");
        }

        var updated = await service.SetWorkingHoursAsync(id, day, request, cancellationToken);
        return updated ? NoContent() : NotFound("Vet not found.");
    }

}

