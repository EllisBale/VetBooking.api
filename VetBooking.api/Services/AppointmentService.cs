using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;

namespace VetBooking.api.Services;

public class AppointmentService(AppDbContext context) : IAppointmentService
{

    public async Task<List<AppointmentResponse>> GetAllAsync()
    {
        return await context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Vet)
            .Select(app => new AppointmentResponse
            {
                Id = app.Id,
                StartUtc = app.StartUtc,
                EndUtc = app.EndUtc,
                Status = new AppointmentStatusResponse { Status = app.Status },
                Notes = app.Notes,
                Pet = new PetResponse
                {
                    Id = app.Pet.Id,
                    Name = app.Pet.Name,
                    Species = app.Pet.Species,
                    Breed = app.Pet.Breed,
                    DateOfBirth = app.Pet.DateOfBirth,
                    OwnerId = app.Pet.OwnerId
                },

                Vet = new VetResponse
                {
                    Id = app.Vet.Id,
                    Name = app.Vet.Name,
                    Specialisation = app.Vet.Specialisation
                }

            }).ToListAsync();
    }
}

