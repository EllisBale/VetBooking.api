using Microsoft.AspNetCore.Http.HttpResults;
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
    
}

