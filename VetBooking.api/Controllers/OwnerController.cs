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
    }
  
