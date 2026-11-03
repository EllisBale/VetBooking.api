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


    public async Task<AppointmentResponse?> GetByIdAsync(int id)
    {
        var appointment = await context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Vet)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment is null)
        {
            return null;
        }

        return new AppointmentResponse
        {
            Id = appointment.Id,
            StartUtc = appointment.StartUtc,
            EndUtc = appointment.EndUtc,
            Status = new AppointmentStatusResponse { Status = appointment.Status },
            Notes = appointment.Notes,
            Pet = new PetResponse
            {
                Id = appointment.Pet.Id,
                Name = appointment.Pet.Name,
                Species = appointment.Pet.Species,
                Breed = appointment.Pet.Breed,
                DateOfBirth = appointment.Pet.DateOfBirth,
                OwnerId = appointment.Pet.OwnerId
            },
            Vet = new VetResponse
            {
                Id = appointment.Vet.Id,
                Name = appointment.Vet.Name,
                Specialisation = appointment.Vet.Specialisation
            }
        };
    }
}

