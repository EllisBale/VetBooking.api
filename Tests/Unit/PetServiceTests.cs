using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;
using VetBooking.api.Services;
using Xunit;


namespace Tests.Unit;

    public class PetServiceTests
    {

        [Fact]
        public async Task GetAllAsync_ReturnsAllPets()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
            .Options;

            var context = new AppDbContext(options);

            context.Owners.Add(new Owner
            {
                Id = 1,
                Name = "John Doe",
                Email = "JohnDoe@example.com",
                Address = "JohnDoe Road",
                PhoneNumber = "07700 900000"
            });

            context.AddRange(
                new Pet
                {
                    Id = 1,
                    Name = "John",
                    Species = "Dog",
                    Breed = "Border collie",
                    DateOfBirth = new DateOnly(2022, 2, 13),
                    OwnerId = 1

                },
                new Pet
                {
                    Id = 2,
                    Name = "Bob",
                    Species = "Dog",
                    Breed = "Border collie",
                    DateOfBirth = new DateOnly(2020, 6, 10),
                    OwnerId = 1
                }

           );

            await context.SaveChangesAsync(TestContext.Current.CancellationToken);

            var service = new PetService(context);

            var result = await service.GetAllAsync(CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnsPet_WhenExists()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
            .Options;

            var context = new AppDbContext(options);

            context.Owners.Add(new Owner
            {
                Id = 1,
                Name = "John Doe",
                Email = "JohnDoe@example.com",
                Address = "JohnDoe Road",
                PhoneNumber = "07700 900000"
            });

            context.Pets.Add(new Pet
            {
                Id = 1,
                Name = "John",
                Species = "Dog",
                Breed = "Border collie",
                DateOfBirth = new DateOnly(2022, 2, 13),
                OwnerId = 1
            });

            await context.SaveChangesAsync(TestContext.Current.CancellationToken);

            var service = new PetService(context);

            var result = await service.GetByIdAsync(1, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            
        }


        [Fact]
        public async Task CreateAsync_ReturnsPet()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
            .Options;

            var context = new AppDbContext(options);

            var service = new PetService(context);

            context.Owners.Add(new Owner
            {
                Id = 1,
                Name = "John Doe",
                Email = "JohnDoe@example.com",
                Address = "JohnDoe Road",
                PhoneNumber = "07700 900000"
            });

            await context.SaveChangesAsync(TestContext.Current.CancellationToken);

            var request = new CreatePetRequest
                {
                    Name = "Bob",
                    Species = "Cat",
                    Breed = "Tabby",
                    DateOfBirth = new DateOnly(2019, 2, 18),
                    OwnerId = 1
                };

            var result = await service.CreateAsync(request, CancellationToken.None);

            Assert.Equal(request.Name, result?.Name);
            Assert.Equal(request.Species, result?.Species);
            Assert.Equal(request.Breed, result?.Breed);
            Assert.Equal(request.DateOfBirth, result?.DateOfBirth);
            Assert.Equal(request.OwnerId, result?.OwnerId);

        }


        [Fact]
        public async Task DeleteAsync_ExistingId()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
              .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
              .Options;

            var context = new AppDbContext(options);

            context.Owners.Add(new Owner
            {
                Id = 1,
                Name = "John Doe",
                Email = "JohnDoe@example.com",
                Address = "JohnDoe Road",
                PhoneNumber = "07700 900000"
            });

            context.Pets.Add(new Pet
            {
                Id = 1,
                Name = "Bob",
                Species = "Cat",
                Breed = "Tabby",
                DateOfBirth = new DateOnly(2019, 2, 18),
                OwnerId = 1
            });

            await context.SaveChangesAsync(TestContext.Current.CancellationToken);

            var service = new PetService(context);

            await service.DeleteAsync(1, CancellationToken.None);

            var result = await context.Pets.FindAsync([1], TestContext.Current.CancellationToken);

            Assert.Null(result);
            
        }


        [Fact]
        public async Task UpdateAsync_ReturnNewPetData()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
              .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
              .Options;

            var context = new AppDbContext(options);

            context.Owners.Add(new Owner
            {
                Id = 1,
                Name = "John Doe",
                Email = "JohnDoe@example.com",
                Address = "JohnDoe Road",
                PhoneNumber = "07700 900000"
            });

            context.Pets.Add(new Pet
            {
                Id = 1,
                Name = "Bob",
                Species = "Cat",
                Breed = "Tabby",
                DateOfBirth = new DateOnly(2019, 2, 18),
                OwnerId = 1
            });

            await context.SaveChangesAsync(TestContext.Current.CancellationToken);

            var service = new PetService(context);

            var request = new UpdatePetRequest
            {
                Name = "Dan",
                Species = "Cat",
                Breed = "Tabby",
                DateOfBirth = new DateOnly(2019, 2, 18),
                OwnerId = 1
            };

            await service.UpdateAsync(1, request, CancellationToken.None);

            var result = await context.Pets.FindAsync([1], TestContext.Current.CancellationToken);

            Assert.NotNull(result);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Species, result.Species);
            Assert.Equal(request.Breed, result.Breed);
            Assert.Equal(request.DateOfBirth, result.DateOfBirth);
            Assert.Equal(request.OwnerId, result.OwnerId);
    }

    }

