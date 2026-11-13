using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;
using VetBooking.api.Services;
using Xunit;

namespace Tests;

    public class AppointmentServiceTests
    {


        [Fact]
        public async Task GetAllAsync_ReturnsAllAppointments()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Vets.Add(new Vet { Id = 1, Name = "Dr. Doe", Specialisation = "Surgery" });
            context.Pets.Add(new Pet { Id = 1, Name = "Buddy", Species = "Dog", Breed = "Labrador", DateOfBirth = new DateOnly(2020, 1, 1), OwnerId = 1 });
            context.AppointmentTypes.Add(new AppointmentType { Id = 1, Name = "Surgery", DurationInMinutes = 30, Price = 50.0m });
            
            context.Appointments.AddRange(
                new Appointment
                {
                    Id = 1,
                    VetId = 1,
                    AppointmentTypeId = 1,
                    PetId = 1,
                    StartUtc = new DateTime(2026, 6, 1, 10, 0, 0),
                    EndUtc = new DateTime(2026, 6, 1, 10, 30, 0),
                    Status = AppointmentStatus.Booked,
                    Notes = "First appointment"
                },
                new Appointment
                {
                    Id = 2,
                    VetId = 1,
                    AppointmentTypeId = 1,
                    PetId = 1,
                    StartUtc = new DateTime(2026, 6, 1, 11, 0, 0),
                    EndUtc = new DateTime(2026, 6, 1, 11, 30, 0),
                    Status = AppointmentStatus.Booked,
                    Notes = "Second appointment"
                }
            );


            await context.SaveChangesAsync(TestContext.Current.CancellationToken);


            var service = new AppointmentService(context);

            var result = await service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }


        [Fact]
        public async Task GetByIdAsync_ReturnsAppointment_WhenExists()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Vets.Add(new Vet { Id = 1, Name = "Dr. Doe", Specialisation = "Surgery" });
            context.Pets.Add(new Pet { Id = 1, Name = "Buddy", Species = "Dog", Breed = "Labrador", DateOfBirth = new DateOnly(2020, 1, 1), OwnerId = 1 });
            context.AppointmentTypes.Add(new AppointmentType { Id = 1, Name = "Surgery", DurationInMinutes = 30, Price = 50.0m });

            context.Appointments.Add(new Appointment
            {
                Id = 1,
                VetId = 1,
                AppointmentTypeId = 1,
                PetId = 1,
                StartUtc = new DateTime(2026, 6, 1, 10, 0, 0),
                EndUtc = new DateTime(2026, 6, 1, 10, 30, 0),
                Status = AppointmentStatus.Booked,
                Notes = "First appointment"
            });

            await context.SaveChangesAsync(TestContext.Current.CancellationToken);
            var service = new AppointmentService(context);

            var result = await service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }




        [Fact]
        public async Task CreateAsync_ThrowsException_WhenAppointmentOverlaps()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
                .Options;

           var context = new AppDbContext(options);

            context.Vets.Add(new Vet { Id = 1 });
            context.Pets.Add(new Pet { Id = 1 });
            context.AppointmentTypes.Add(new AppointmentType { Id = 1 });
            

            context.Appointments.Add(new Appointment
            {
                Id = 1,
                VetId = 1,
                AppointmentTypeId = 1,
                PetId = 1,
                StartUtc = new DateTime(2026, 6, 1, 10, 0, 0),
                EndUtc = new DateTime(2026, 6, 1, 11, 0, 0)

            });

             await context.SaveChangesAsync(TestContext.Current.CancellationToken);

            var service = new AppointmentService(context);

            var request = new CreateAppointmentRequest
            {
                VetId = 1,
                PetId = 1,
                AppointmentTypeId = 1,
                StartUtc = new DateTime(2026, 6, 1, 10, 30, 0),
                EndUtc = new DateTime(2026, 6, 1, 11, 30, 0)
            };

            var result = await Assert.ThrowsAsync<InvalidOperationException>(() => service.CreateAsync(request));
        }


        [Fact]
        public async Task CreateAsync_ReturnsAppointment_WhenNoOverlap()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Vets.Add(new Vet { Id = 2 });
            context.Pets.Add(new Pet { Id = 2 });
            context.AppointmentTypes.Add(new AppointmentType { Id = 1 });

            await context.SaveChangesAsync(TestContext.Current.CancellationToken);


        var service = new AppointmentService(context);

            var request = new CreateAppointmentRequest
            {
                VetId = 2,
                PetId = 2,
                AppointmentTypeId = 1,
                StartUtc = new DateTime(2026, 6, 1, 10, 30, 0),
                EndUtc = new DateTime(2026, 6, 1, 11, 30, 0)
            };

            var result = await service.CreateAsync(request);

            Assert.NotNull(result);
            Assert.Equal(request.StartUtc, result.StartUtc);
            Assert.Equal(request.EndUtc, result.EndUtc);


        }


        [Fact]
        public async Task CreateAsync_ThrowsArgumentException_WhenPetDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Vets.Add(new Vet { Id = 1 });
            context.AppointmentTypes.Add(new AppointmentType { Id = 1 });

            await context.SaveChangesAsync(TestContext.Current.CancellationToken);

            var service = new AppointmentService(context);

            var request = new CreateAppointmentRequest
            {
                VetId = 1,
                PetId = 99, 
                AppointmentTypeId = 1,
                StartUtc = new DateTime(2026, 6, 1, 10, 30, 0),
                EndUtc = new DateTime(2026, 6, 1, 11, 30, 0)
            };

            var result = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(request));
            Assert.Contains("Pet ID 99 does not exist.", result.Message);
        }


        [Fact]
        public async Task CreateAsync_ThrowsArgumentException_WhenVetDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Pets.Add(new Pet { Id = 1 });
            context.AppointmentTypes.Add(new AppointmentType { Id = 1 });

            await context.SaveChangesAsync(TestContext.Current.CancellationToken);

            var service = new AppointmentService(context);

            var request = new CreateAppointmentRequest
            {
                VetId = 99,
                PetId = 1,
                AppointmentTypeId = 1,
                StartUtc = new DateTime(2026, 6, 1, 10, 30, 0),
                EndUtc = new DateTime(2026, 6, 1, 11, 30, 0)
            };

            var result = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(request));
            Assert.Contains("Vet ID 99 does not exist.", result.Message);
        }


        [Fact]
        public async Task CreateAsync_ThrowsArgumentException_WhenAppointmentTypeDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Vets.Add(new Vet { Id = 1 });
            context.Pets.Add(new Pet { Id = 1 });

            await context.SaveChangesAsync(TestContext.Current.CancellationToken);

            var service = new AppointmentService(context);

            var request = new CreateAppointmentRequest
            {
                VetId = 1,
                PetId = 1,
                AppointmentTypeId = 99,
                StartUtc = new DateTime(2026, 6, 1, 10, 30, 0),
                EndUtc = new DateTime(2026, 6, 1, 11, 30, 0)
            };

            var result = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(request));
            Assert.Contains("Appointment Type ID 99 does not exist.", result.Message);
        }



    }

