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
        public async Task CreateAsync_ThrowsException_WhenAppointmentOverlaps()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
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

            await context.SaveChangesAsync();

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
    }

