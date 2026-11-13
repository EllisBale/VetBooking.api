using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;

namespace VetBooking.api.Services;

public class AppointmentService(AppDbContext context) : IAppointmentService
{

    public async Task<List<AppointmentResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Vet)
            .Include(a => a.AppointmentType)
            .Select(app => new AppointmentResponse
            {
                Id = app.Id,
                StartUtc = app.StartUtc,
                EndUtc = app.EndUtc,
                Status = app.Status,
                Notes = app.Notes,

                AppointmentType = new AppointmentTypeResponse
                {
                    Id = app.AppointmentType.Id,
                    Name = app.AppointmentType.Name,
                    DurationInMinutes = app.AppointmentType.DurationInMinutes,
                    Price = app.AppointmentType.Price
                },

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

            }).ToListAsync(cancellationToken);
    }


    public async Task<AppointmentResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var appointment = await context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Vet)
            .Include(a => a.AppointmentType)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (appointment is null)
        {
            return null;
        }

        return new AppointmentResponse
        {
            Id = appointment.Id,
            StartUtc = appointment.StartUtc,
            EndUtc = appointment.EndUtc,
            Status = appointment.Status,
            Notes = appointment.Notes,

            AppointmentType = new AppointmentTypeResponse
            {
                Id = appointment.AppointmentType.Id,
                Name = appointment.AppointmentType.Name,
                DurationInMinutes = appointment.AppointmentType.DurationInMinutes,
                Price = appointment.AppointmentType.Price
            },

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


    public async Task<bool> HasAppointmentOverlapAsync(int vetId, DateTime requestedStart, DateTime requestedEnd)
        {
            return await context.Appointments
                .Where(a => a.VetId == vetId && a.Status != AppointmentStatus.Cancelled)
                .AnyAsync(a => requestedStart < a.EndUtc && requestedEnd > a.StartUtc);
        }
        

    public async Task<AppointmentResponse?> CreateAsync(CreateAppointmentRequest request, CancellationToken cancellationToken)
    {

        if (request is null)
        {
            throw new ArgumentException("Appointment request cannot be null.");
        }

        if (request.StartUtc >= request.EndUtc)
        {
            throw new ArgumentException("Start time must be before end time.");
        }

        if (request.PetId is null || request.PetId <= 0 ||
            request.VetId is null || request.VetId <= 0 ||
            request.AppointmentTypeId is null || request.AppointmentTypeId <= 0)
        {
            throw new ArgumentException("A valid PetId, VetId, and AppointmentTypeId are required.");
        }

        var petExists = await context.Pets.AnyAsync(p => p.Id == request.PetId);
        var vetExists = await context.Vets.AnyAsync(v => v.Id == request.VetId);
        var appointmentTypeExists = await context.AppointmentTypes.AnyAsync(at => at.Id == request.AppointmentTypeId);

        if (!petExists)
        {
            throw new ArgumentException($"Pet ID {request.PetId} does not exist.");
        }

        if (!vetExists)
        {
            throw new ArgumentException($"Vet ID {request.VetId} does not exist.");
        }

        if (!appointmentTypeExists)
        {
            throw new ArgumentException($"Appointment Type ID {request.AppointmentTypeId} does not exist.");
        }


        var hasOverlap = await HasAppointmentOverlapAsync(request.VetId.Value, request.StartUtc, request.EndUtc);

        if (hasOverlap)
        {
            throw new InvalidOperationException("The selected time slot conflicts with an existing appointment.");
        }

        var appointment = new Appointment
        {
            StartUtc = request.StartUtc,
            EndUtc = request.EndUtc,
            Status = request.Status,
            Notes = request.Notes,
            PetId = request.PetId ?? 0,
            VetId = request.VetId ?? 0,
            AppointmentTypeId = request.AppointmentTypeId ?? 0
        };


        try
        {
            context.Appointments.Add(appointment);
            await context.SaveChangesAsync(cancellationToken);
        }

        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("An error occurred while creating the appointment.", ex);
        }

        var createdAppointment = await GetByIdAsync(appointment.Id, cancellationToken);
        return createdAppointment!;

    }
}

