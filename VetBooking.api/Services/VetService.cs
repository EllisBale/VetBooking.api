using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;

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


    public async Task<VetResponse> CreateAsync(CreateVetRequest request)
    {
        var vet = new Vet
        {
            Name = request.Name,
            Specialisation = request.Specialisation
        };

        context.Vets.Add(vet);
        await context.SaveChangesAsync();

        return new VetResponse
        {
            Id = vet.Id,
            Name = vet.Name,
            Specialisation = vet.Specialisation
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var vet = await context.Vets.FindAsync(id);

        if (vet is null)
        {
            return false;
        }

        context.Vets.Remove(vet);
        await context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> UpdateAsync(int id, UpdateVetRequest request)
    {
        var vet = await context.Vets.FindAsync(id);

        if (vet is null)
        {
            return false;
        }

        vet.Name = request.Name;
        vet.Specialisation = request.Specialisation;

        await context.SaveChangesAsync();

        return true;
    }
}


