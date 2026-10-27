using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;

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

    public async Task<OwnerResponse?> GetByIdAsync(int id)
        => await context.Owners
            .Where(owner => owner.Id == id)
            .Select(owner => new OwnerResponse
            {
                Id = owner.Id,
                Name = owner.Name,
                Address = owner.Address,
                PhoneNumber = owner.PhoneNumber
            }).FirstOrDefaultAsync();


    public async Task<OwnerResponse> CreateAsync(CreateOwnerRequest request)
    {
        var owner = new Owner
        {
            Name = request.Name,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber
        };

        context.Owners.Add(owner);
        await context.SaveChangesAsync();

        return new OwnerResponse
        {
            Id = owner.Id,
            Name = owner.Name,
            Address = owner.Address,
            PhoneNumber = owner.PhoneNumber
        };
    }
            
}
