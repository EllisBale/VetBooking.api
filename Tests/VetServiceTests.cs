using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;
using VetBooking.api.Services;
using Xunit;

namespace Tests;

public class VetServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllVets()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        context.Vets.AddRange(
            new Vet
            {
                Id = 1,
                Name = "Dr. Smith",
                Specialisation = "Surgery",
                Email = "Vet@example.com",
                PhoneNumber = "07700 900000"
            },
            new Vet
            {
                Id = 2,
                Name = "Dr. Johnson",
                Specialisation = "Dentistry",
                Email = "Vet2@example.com",
                PhoneNumber = "07700 900001"
            }
        );

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var service = new VetService(context);

        var result = await service.GetAllAsync(CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }


    [Fact]
    public async Task GetByIdAsync_ReturnsVet_WhenExists()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        context.Vets.Add(new Vet
        {
            Id = 1,
            Name = "Dr. Smith",
            Specialisation = "Surgery",
            Email = "Vet@example.com",
            PhoneNumber = "07700 900000"
        });

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var service = new VetService(context);

        var result = await service.GetByIdAsync(1, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }


    [Fact]
    public async Task CreateAsync_ReturnsVet()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
           .Options;

        var context = new AppDbContext(options);

        var service = new VetService(context);

        var request = new CreateVetRequest
        {
            Name = "Dr. Smith",
            Specialisation = "Surgery",
            Email = "Vet@example.com",
            PhoneNumber = "07700 900000"
        };

        var result = await service.CreateAsync(request, CancellationToken.None);

        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Specialisation, result.Specialisation);
        Assert.Equal(request.Email, result.Email);
        Assert.Equal(request.PhoneNumber, result.PhoneNumber);

    }


    [Fact]
    public async Task CreateAsync_ThrowsException_WhenEmailExists()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
               .Options;

        var context = new AppDbContext(options);

        context.Vets.Add(new Vet
        {
            Id = 1,
            Name = "Dr. Smith",
            Specialisation = "Surgery",
            Email = "Vet@example.com",
            PhoneNumber = "07700 900000"

        });

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var service = new VetService(context);

        var request = new CreateVetRequest
        {
            Name = "Dr. Johnson",
            Specialisation = "Dentistry",
            Email = "Vet@example.com",
            PhoneNumber = "07700 900001"

        };

        var result = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(request, CancellationToken.None));

        Assert.Contains($"A vet with the email {request.Email} already exists.", result.Message);
    }

}

