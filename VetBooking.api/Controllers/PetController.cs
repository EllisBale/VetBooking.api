using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;

namespace VetBooking.api.Controllers;

    [ApiController]
    [Route("api/[controller]")]

    public class PetController(IPetService service) : ControllerBase
    {
        public async Task<ActionResult<List<PetResponse>>> GetAllPets()
        {
            var pets = await service.GetAllAsync();

            if (pets is null || pets.Count is 0)
            {
                return NotFound("No Pets found.");
            }

            return Ok(pets);
        }
    }

