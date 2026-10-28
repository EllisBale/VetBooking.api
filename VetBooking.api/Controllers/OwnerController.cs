using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;
namespace VetBooking.api.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController(IOwnerService service) : ControllerBase
    {
        [HttpGet]

        public async Task<ActionResult<List<OwnerResponse>>> GetAllOwners()
        {
            var owners = await service.GetAllAsync();

            if (owners == null || owners.Count == 0)
            {
                return NotFound("No Owners found.");
            }

            return Ok(owners);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerResponse?>> GetById(int id)
        {
            var owner = await service.GetByIdAsync(id);

            if (owner is null)
            {
                return NotFound($"Owner with ID {id} not found.");
            }

            return Ok(owner);
        }



        [HttpPost]

        public async Task<ActionResult<OwnerResponse>> CreateOwner(CreateOwnerRequest request)
        {
        var createdOwner = await service.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = createdOwner.Id }, createdOwner);
    }
    }
  
