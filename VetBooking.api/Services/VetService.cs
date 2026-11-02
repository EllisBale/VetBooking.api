using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;

namespace VetBooking.api.Services;

public class VetService(AppDbContext context) : IVetService
{
    public async Task<List<VetResponse>> GetAllAsync()
        => await context.Vets
                .Select(vet => new VetResponse
                {
                    Id = vet.Id,
                    Name = vet.Name,
                    Specialisation = vet.Specialisation
                })
                .ToListAsync();

    public async Task<VetResponse?> GetByIdAsync(int id)
        => await context.Vets
                .Where(vet => vet.Id == id)
                .Select(vet => new VetResponse
                {
                    Id = vet.Id,
                    Name = vet.Name,
                    Specialisation = vet.Specialisation
                })
                .FirstOrDefaultAsync();
}


