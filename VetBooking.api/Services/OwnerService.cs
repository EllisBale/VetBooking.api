using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;

namespace VetBooking.api.Services;

public class OwnerService(AppDbContext context) : IOwnerService
{
    public async Task<List<OwnerResponse>> GetAllAsync(CancellationToken cancellationToken)
       => await context.Owners
             .Select(owner => new OwnerResponse
             {
                 Id = owner.Id,
                 Name = owner.Name,
                 Email = owner.Email,
                 Address = owner.Address,
                 PhoneNumber = owner.PhoneNumber

             })
            .ToListAsync(cancellationToken);

    public async Task<OwnerResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await context.Owners
            .Where(owner => owner.Id == id)
            .Select(owner => new OwnerResponse
            {
                Id = owner.Id,
                Name = owner.Name,
                Email = owner.Email,
                Address = owner.Address,
                PhoneNumber = owner.PhoneNumber
            }).FirstOrDefaultAsync(cancellationToken);


    public async Task<OwnerResponse> CreateAsync(CreateOwnerRequest request, CancellationToken cancellationToken)
    {
        if (await context.Owners.AnyAsync(o => o.Email == request.Email, cancellationToken))
        {
            throw new ArgumentException($"An owner with the email {request.Email} already exists.");
        }

        var owner = new Owner
        {
            Name = request.Name,
            Email = request.Email,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber
        };


        try
        {
            context.Owners.Add(owner);
            await context.SaveChangesAsync(cancellationToken);
        }

        catch (DbUpdateException ex)
        {
            throw new ArgumentException("An Owner with this email already exists.", ex);
        }

        return new OwnerResponse
        {
            Id = owner.Id,
            Name = owner.Name,
            Email = owner.Email,
            Address = owner.Address,
            PhoneNumber = owner.PhoneNumber
        };
    }


    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var owner = await context.Owners.FindAsync(id, cancellationToken);

        if (owner is null) {

            return false;
        }


        try
        {
            context.Owners.Remove(owner);
            await context.SaveChangesAsync(cancellationToken);
        }

        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Could not delete this Owner due to existing database constraints.", ex);
        }

        return true;
    }


    public async Task<bool> UpdateAsync(int id, UpdateOwnerRequest request, CancellationToken cancellationToken)
    {
        var owner = await context.Owners.FindAsync(id, cancellationToken);

        if (owner is null)
        {
            return false;
        }

        if (await context.Owners.AnyAsync(o => o.Email == request.Email && o.Id != id, cancellationToken))
        {
            throw new ArgumentException($"An owner with the email {request.Email} already exists.");
        }

        owner.Name = request.Name;
        owner.Email = request.Email;
        owner.Address = request.Address;
        owner.PhoneNumber = request.PhoneNumber;

        try
        {
           await context.SaveChangesAsync(cancellationToken);
        }

        catch (DbUpdateException ex)
        {
            throw new ArgumentException("An Owner with this email already exists.", ex);
        }

        return true;

    }

}
