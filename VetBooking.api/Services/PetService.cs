using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;

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


        public async Task<PetResponse?> GetByIdAsync(int id)
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
                    .FirstOrDefaultAsync();


        public async Task<PetResponse?> CreateAsync(CreatePetRequest request)
        {

            var ownerExists = await context.Owners.AnyAsync(owner => owner.Id == request.OwnerId);

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

            context.Pets.Add(pet);
                 await context.SaveChangesAsync();

                 return new PetResponse
                 {
                   Name = pet.Name,
                   Species = pet.Species,
                   Breed = pet.Breed,
                   DateOfBirth = pet.DateOfBirth,
                   OwnerId = pet.OwnerId
                 };
        }

       public async Task<bool> DeleteAsync(int id)
       {
            var pet = await context.Pets.FindAsync(id);

            if (pet is null)
            {
                return false;
            }

            context.Pets.Remove(pet);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(int id, UpdatePetRequest request)
        {
            var pet = await context.Pets.FindAsync(id);
            var ownerExists = await context.Owners.AnyAsync(owner => owner.Id == request.OwnerId);

            if (pet is null || !ownerExists)
            {
               return false;
            }

            pet.Name = request.Name;
            pet.Species = request.Species;
            pet.Breed = request.Breed;
            pet.DateOfBirth = request.DateOfBirth;
            pet.OwnerId = request.OwnerId;

            await context.SaveChangesAsync();

            return true;
        }
        
    }

