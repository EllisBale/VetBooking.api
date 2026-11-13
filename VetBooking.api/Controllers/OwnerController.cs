using Microsoft.AspNetCore.Mvc;
using VetBooking.api.Dtos;
using VetBooking.api.Services;
namespace VetBooking.api.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController(IOwnerService service) : ControllerBase
    {
        [HttpGet]

        public async Task<ActionResult<List<OwnerResponse>>> GetAllOwners(CancellationToken cancellationToken)
        {
            var owners = await service.GetAllAsync(cancellationToken);

            if (owners is null || owners.Count is 0)
            {
                return NotFound("No Owners found.");
            }

            return Ok(owners);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerResponse?>> GetById(int id, CancellationToken cancellationToken)
        {
            var owner = await service.GetByIdAsync(id, cancellationToken);

            if (owner is null)
            {
                return NotFound($"Owner with ID {id} not found.");
            }

            return Ok(owner);
        }



        [HttpPost]

        public async Task<ActionResult<OwnerResponse>> CreateOwner(CreateOwnerRequest request, CancellationToken cancellationToken)
        {
            var createdOwner = await service.CreateAsync(request, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = createdOwner.Id }, createdOwner);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteOwner(int id, CancellationToken cancellationToken)
        {
            var owner = await service.GetByIdAsync(id, cancellationToken);

            if (owner is null)
            {
                return NotFound("Owner Not Found.");
            }

            var deleted = await service.DeleteAsync(id, cancellationToken);

            if (!deleted)
            {
                return BadRequest("Failed to delete Owner.");
            }

            return NoContent();
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateOwner(int id, UpdateOwnerRequest request, CancellationToken cancellationToken)
        {
            var owner = await service.GetByIdAsync(id, cancellationToken);

            if (owner is null)
            {
                return NotFound("Owner Not Found.");
            }

            var updated = await service.UpdateAsync(id, request, cancellationToken);

            if (!updated)
            {
                return BadRequest("Failed to update Owner.");
            }

            return NoContent();
        }


    }
  
