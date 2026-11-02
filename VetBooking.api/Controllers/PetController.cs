using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;

namespace VetBooking.api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PetController(IPetService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PetResponse>>> GetAllPets()
    {
        var pets = await service.GetAllAsync();

        if (pets is null || pets.Count is 0)
        {
            return NotFound("No Pets found.");
        }

        return Ok(pets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PetResponse?>> GetById(int id)
    {
        var pet = await service.GetByIdAsync(id);

        if (pet is null)
        {
            return NotFound($"Pet with ID {id} not found.");
        }

        return Ok(pet);

    }


    [HttpPost]

    public async Task<ActionResult<PetResponse>> CreatePet(CreatePetRequest request)
    {
        var createdPet = await service.CreateAsync(request);

        if (createdPet is null)
        {
            return BadRequest("Pet could not be created.");
        }

        return CreatedAtAction(nameof(GetById), new { id = createdPet.Id }, createdPet);
    }


    [HttpDelete("{id}")]

    public async Task<IActionResult> DeletePet(int id)
    {
        var pet = await service.GetByIdAsync(id);

        if (pet is null)
        {
            return NotFound("Pet Not Found.");
        }

        var deleted = await service.DeleteAsync(id);

        if (!deleted)
        {
            return BadRequest("Failed to delete Pet");
        }

        return NoContent();
    }


    [HttpPut("{id}")]

    public async Task<IActionResult> UpdatePet(int id, UpdatePetRequest request)
    {
        var pet = await service.GetByIdAsync(id);

        if (pet is null)
        {
            return NotFound("Pet Not Found.");
        }

        var updated = await service.UpdateAsync(id, request);

        if (!updated)
        {
            return BadRequest("Failed to update Pet");
        }

        return NoContent();
    }

}

 