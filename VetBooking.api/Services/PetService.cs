using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;

namespace VetBooking.api.Services;

    public class PetService(AppDbContext context) : IPetService
    {
        public async Task<List<PetResponse>> GetAllAsync()
            => await context.Pets
                    .Select(pet => new PetResponse
                    {
                        Id = pet.Id,
                        Name = pet.Name,
                        Species = pet.Species,
                        Breed = pet.Breed,
                        DateOfBirth = pet.DateOfBirth,
                        OwnerId = pet.OwnerId
                    })
                    .ToListAsync();

        
    }

