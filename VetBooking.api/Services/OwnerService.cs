using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;

namespace VetBooking.api.Services;

public class OwnerService(AppDbContext context) : IOwnerService
{
    public async Task<List<OwnerResponse>> GetAllAsync()
       => await context.Owners
             .Select(owner => new OwnerResponse
             {
                 Id = owner.Id,
                 Name = owner.Name,
                 Address = owner.Address,
                 PhoneNumber = owner.PhoneNumber

             })
            .ToListAsync();
}
