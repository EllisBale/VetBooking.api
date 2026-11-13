using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;

namespace VetBooking.api.Services;

    public class PetService(AppDbContext context) : IPetService
    {
        public async Task<List<PetResponse>> GetAllAsync(CancellationToken cancellationToken)
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
                    .ToListAsync(cancellationToken);


        public async Task<PetResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
            => await context.Pets
                    .Where(pet => pet.Id == id)
                    .Select(pet => new PetResponse
                    {
                        Id = pet.Id,
                        Name = pet.Name,
                        Species = pet.Species,
                        Breed = pet.Breed,
                        DateOfBirth = pet.DateOfBirth,
                        OwnerId = pet.OwnerId
                    })
                    .FirstOrDefaultAsync(cancellationToken);


        public async Task<PetResponse?> CreateAsync(CreatePetRequest request, CancellationToken cancellationToken)
        {

            var ownerExists = await context.Owners.AnyAsync(owner => owner.Id == request.OwnerId, cancellationToken);

            if (!ownerExists)
            {
                return null;
            }

            var pet = new Pet
            {
                Name = request.Name,
                Species = request.Species,
                Breed = request.Breed,
                DateOfBirth = request.DateOfBirth,
                OwnerId = request.OwnerId ?? 0
            };

            try
            {
                context.Pets.Add(pet);
                await context.SaveChangesAsync(cancellationToken);
            }

            catch (DbUpdateException ex)
            {
                throw new ArgumentException("An error occurred while creating the pet.", ex);
            }


            return new PetResponse
            {
                Name = pet.Name,
                Species = pet.Species,
                Breed = pet.Breed,
                DateOfBirth = pet.DateOfBirth,
                OwnerId = pet.OwnerId
            };
        }


       public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
       {
            var pet = await context.Pets.FindAsync(id, cancellationToken);

            if (pet is null)
            {
                return false;
            }

            try
            {
                context.Pets.Remove(pet);
                await context.SaveChangesAsync(cancellationToken);
            }
            
            catch (DbUpdateException ex)
            {
                throw new ArgumentException("Could not delete this pet due to existing database constraints.", ex);
            }

            return true;
       }


        public async Task<bool> UpdateAsync(int id, UpdatePetRequest request, CancellationToken cancellationToken)
        {
            var pet = await context.Pets.FindAsync(id, cancellationToken);
            var ownerExists = await context.Owners.AnyAsync(owner => owner.Id == request.OwnerId, cancellationToken);

            if (pet is null || !ownerExists)
            {
               return false;
            }

            pet.Name = request.Name;
            pet.Species = request.Species;
            pet.Breed = request.Breed;
            pet.DateOfBirth = request.DateOfBirth;
            pet.OwnerId = request.OwnerId;

            try
            {
               await context.SaveChangesAsync(cancellationToken);
            }

            catch (DbUpdateException ex)
            {
                throw new ArgumentException("An error occurred while updating the pet.", ex);
            }

            return true;
        }
        
    }

