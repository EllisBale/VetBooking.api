using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;

namespace VetBooking.api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PetController(IPetService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PetResponse>>> GetAllPets(CancellationToken cancellationToken)
    {
        var pets = await service.GetAllAsync(cancellationToken);

        if (pets is null || pets.Count is 0)
        {
            return NotFound("No Pets found.");
        }

        return Ok(pets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PetResponse?>> GetById(int id, CancellationToken cancellationToken)
    {
        var pet = await service.GetByIdAsync(id, cancellationToken);

        if (pet is null)
        {
            return NotFound($"Pet with ID {id} not found.");
        }

        return Ok(pet);

    }


    [HttpPost]

    public async Task<ActionResult<PetResponse>> CreatePet(CreatePetRequest request, CancellationToken cancellationToken)
    {
        var createdPet = await service.CreateAsync(request, cancellationToken);

        if (createdPet is null)
        {
            return BadRequest("Pet could not be created.");
        }

        return CreatedAtAction(nameof(GetById), new { id = createdPet.Id }, createdPet);
    }


    [HttpDelete("{id}")]

    public async Task<IActionResult> DeletePet(int id, CancellationToken cancellationToken)
    {
        var pet = await service.GetByIdAsync(id, cancellationToken);

        if (pet is null)
        {
            return NotFound("Pet Not Found.");
        }

        var deleted = await service.DeleteAsync(id, cancellationToken);

        if (!deleted)
        {
            return BadRequest("Failed to delete Pet");
        }

        return NoContent();
    }


    [HttpPut("{id}")]

    public async Task<IActionResult> UpdatePet(int id, UpdatePetRequest request, CancellationToken cancellationToken)
    {
        var pet = await service.GetByIdAsync(id, cancellationToken);

        if (pet is null)
        {
            return NotFound("Pet Not Found.");
        }

        var updated = await service.UpdateAsync(id, request, cancellationToken);

        if (!updated)
        {
            return BadRequest("Failed to update Pet");
        }

        return NoContent();
    }

}

 