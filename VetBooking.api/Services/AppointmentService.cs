using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;

namespace VetBooking.api.Services;

public class AppointmentService(AppDbContext context) : IAppointmentService
{

    public async Task<List<AppointmentDetailResponse>> GetAllAsync(CancellationToken ct)
    {
        return await context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Vet)
            .Include(a => a.Owner)
            .Include(a => a.AppointmentType)
            .Select(app => new AppointmentDetailResponse
            {
                Id = app.Id,
                StartUtc = app.StartUtc,
                EndUtc = app.EndUtc,
                Status = app.Status,
                Notes = app.Notes,
                ExamRoom = app.ExamRoom,
                OwnerId = app.OwnerId,
                OwnerName = app.Owner.Name,
                OwnerPhoneNumber = app.Owner.PhoneNumber,

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

            }).ToListAsync(ct);
    }


    public async Task<AppointmentDetailResponse?> GetByIdAsync(int id, CancellationToken ct)
    {
        var app = await context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Vet)
            .Include(a => a.Owner)
            .Include(a => a.AppointmentType)
            .FirstOrDefaultAsync(a => a.Id == id, ct);

        if (app is null)
        {
            return null;
        }

        return new AppointmentDetailResponse
        {
            Id = app.Id,
            StartUtc = app.StartUtc,
            EndUtc = app.EndUtc,
            Status = app.Status,
            Notes = app.Notes,
            ExamRoom = app.ExamRoom,
            OwnerId = app.OwnerId,
            OwnerName = app.Owner.Name,
            OwnerPhoneNumber = app.Owner.PhoneNumber,

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
        };
    }


    public async Task<bool> HasAppointmentOverlapAsync(int vetId, int petId, DateTime reqStart, DateTime reqEnd)
        {
            return await context.Appointments
                .Where(a => (a.VetId == vetId || a.PetId == petId) && a.Status != AppointmentStatus.Cancelled)
                .AnyAsync(a => reqStart < a.EndUtc && reqEnd > a.StartUtc);
        }
        

    public async Task<AppointmentDetailResponse?> CreateAsync(CreateAppointmentRequest req, CancellationToken ct)
    {

        if (req is null)
        {
            throw new ArgumentException("Appointment request cannot be null.");
        }

        if (req.StartUtc >= req.EndUtc)
        {
            throw new ArgumentException("Start time must be before end time.");
        }

        if (req.PetId is null || req.PetId <= 0 ||
            req.VetId is null || req.VetId <= 0 ||
            req.AppointmentTypeId is null || req.AppointmentTypeId <= 0)
        {
            throw new ArgumentException("A valid PetId, VetId, and AppointmentTypeId are required.");
        }

        var pet = await context.Pets.FirstOrDefaultAsync(p => p.Id == req.PetId, ct);
        var vetExists = await context.Vets.AnyAsync(v => v.Id == req.VetId, ct);
        var appointmentTypeExists = await context.AppointmentTypes.AnyAsync(at => at.Id == req.AppointmentTypeId, ct);

        if (pet is null)
        {
            throw new ArgumentException($"Pet ID {req.PetId} does not exist.");
        }

        if (!vetExists)
        {
            throw new ArgumentException($"Vet ID {req.VetId} does not exist.");
        }

        if (!appointmentTypeExists)
        {
            throw new ArgumentException($"Appointment Type ID {req.AppointmentTypeId} does not exist.");
        }


        var hasOverlap = await HasAppointmentOverlapAsync(req.VetId.Value, req.PetId.Value,  req.StartUtc, req.EndUtc);

        if (hasOverlap)
        {
            throw new InvalidOperationException("The selected time slot conflicts with an existing appointment.");
        }

        var appointment = new Appointment
        {
            StartUtc = req.StartUtc,
            EndUtc = req.EndUtc,
            Status = req.Status,
            Notes = req.Notes,
            PetId = req.PetId ?? 0,
            VetId = req.VetId ?? 0,
            AppointmentTypeId = req.AppointmentTypeId ?? 0,
            ExamRoom = req.ExamRoom,
            OwnerId = pet.OwnerId,
        };


        try
        {

            context.Appointments.Add(appointment);
            await context.SaveChangesAsync(ct);
        }

        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("An error occurred while creating the appointment.", ex);
        }

        var createdAppointment = await GetByIdAsync(appointment.Id, ct);
        return createdAppointment!;

    }


    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var app = await context.Appointments.FindAsync([id], ct);

        if (app is null)
        {
            return false;
        }

        try
        {
            context.Appointments.Remove(app);
            await context.SaveChangesAsync(ct);
        }

        catch (DbUpdateException ex)
        {
            throw new ArgumentException("Could not delete this Appointment due to existing database constraints.", ex);
        }

        return true;
    }



}

