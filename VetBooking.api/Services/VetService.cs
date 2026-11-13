using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;

namespace VetBooking.api.Services;

public class VetService(AppDbContext context) : IVetService
{
    public async Task<List<VetResponse>> GetAllAsync(CancellationToken cancellationToken)
        => await context.Vets
                .Select(vet => new VetResponse
                {
                    Id = vet.Id,
                    Name = vet.Name,
                    Specialisation = vet.Specialisation,
                    Email = vet.Email,
                    PhoneNumber = vet.PhoneNumber
                })  
                .ToListAsync(cancellationToken);

    public async Task<VetResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.Vets
                .Where(vet => vet.Id == id)
                .Select(vet => new VetResponse
                {
                    Id = vet.Id,
                    Name = vet.Name,
                    Specialisation = vet.Specialisation,
                    Email = vet.Email,
                    PhoneNumber = vet.PhoneNumber
                })
                .FirstOrDefaultAsync(cancellationToken);


    public async Task<VetResponse> CreateAsync(CreateVetRequest request, CancellationToken cancellationToken)
    {

        if (await context.Vets.AnyAsync(v => v.Email == request.Email, cancellationToken))
        {
            throw new ArgumentException($"A vet with the email {request.Email} already exists.");
        }

        var vet = new Vet
        {
            Name = request.Name,
            Specialisation = request.Specialisation,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };


        try
        {
            context.Vets.Add(vet);
            await context.SaveChangesAsync(cancellationToken);
        }

        catch (DbUpdateException ex)
        {
            throw new ArgumentException("A vet with this email or phone number already exists.", ex);
        }


        return new VetResponse
        {
            Id = vet.Id,
            Name = vet.Name,
            Specialisation = vet.Specialisation,
            Email = vet.Email,
            PhoneNumber = vet.PhoneNumber
        };
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var vet = await context.Vets.FindAsync(id, cancellationToken);

        if (vet is null)
        {
            return false;
        }


        try
        {
            context.Vets.Remove(vet);
            await context.SaveChangesAsync(cancellationToken);
        }

        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Could not delete this vet due to existing database constraints.", ex);
        }

        return true;
    }


    public async Task<bool> UpdateAsync(int id, UpdateVetRequest request, CancellationToken cancellationToken)
    {
        var vet = await context.Vets.FindAsync(id, cancellationToken);

        if (vet is null)
        {
            return false;
        }

        if (await context.Vets.AnyAsync(v => v.Email == request.Email && v.Id != id, cancellationToken))
        {
            throw new ArgumentException($"A vet with the email {request.Email} already exists.");
        }

        vet.Name = request.Name;
        vet.Specialisation = request.Specialisation;
        vet.Email = request.Email;
        vet.PhoneNumber = request.PhoneNumber;

        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        catch (DbUpdateException ex)
        {
            throw new ArgumentException("A vet with this email or phone number already exists.", ex);
        }

        return true;
    }


    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Vets.AnyAsync(v => v.Id == id);
    }


    public async Task<List<WorkingHoursResponse>> GetWorkingHoursAsync(int vetId, CancellationToken cancellationToken)
    {
        var vetExists = await context.Vets.AnyAsync(v => v.Id == vetId, cancellationToken);

        if (!vetExists)
        {
            throw new ArgumentException($"Vet ID {vetId} does not exist.");
        }

 
        return await context.VetWorkingHours
           .Where(wh => wh.VetId == vetId)
           .OrderBy(wh => wh.DayOfWeek)
           .Select(wh => new WorkingHoursResponse
           {
               DayOfWeek = wh.DayOfWeek,
               StartTime = wh.StartTime,
               EndTime = wh.EndTime
           })
           .ToListAsync(cancellationToken);
    }

    public async Task<bool> SetWorkingHoursAsync(int vetId, DayOfWeek day, SetWorkingHoursRequest request, CancellationToken cancellationToken)
    {
        if (!await context.Vets.AnyAsync(v => v.Id == vetId, cancellationToken))
        {
            return false;
        }

        var hours = await context.VetWorkingHours.FirstOrDefaultAsync(wh => wh.VetId == vetId && wh.DayOfWeek == day, cancellationToken);

        if (hours is null)
        {
            hours = new WorkingHours
            {
                VetId = vetId,
                DayOfWeek = day,
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };
            context.VetWorkingHours.Add(hours);
        }
        else
        {
            hours.StartTime = request.StartTime;
            hours.EndTime = request.EndTime;
        }

        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        catch (DbUpdateException ex)
        {
            throw new ArgumentException("Could not set working hours due to existing database constraints.", ex);
        }

        return true;

    }



}


