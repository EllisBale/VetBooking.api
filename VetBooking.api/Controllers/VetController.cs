using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;

namespace VetBooking.api.Controllers;

    [ApiController]
    [Route("api/[controller]")]

    public class VetController(IVetService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List <VetResponse>>> GetAllVets()
        {
            var vets = await service.GetAllAsync();
            
            if (vets is null || vets.Count is 0)
            {
                return NotFound("No Vets found.");
            }

            return Ok(vets);
        }

    [HttpGet("{id}")]
    public async Task<ActionResult<VetResponse?>> GetById(int id)
    {
        var vet = await service.GetByIdAsync(id);

        if (vet is null)
        {
            return NotFound($"Vet with ID {id} not found.");
        }

        return Ok(vet);
    }


    }

