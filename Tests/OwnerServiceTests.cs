using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;
using VetBooking.api.Services;
using Xunit;

namespace Tests;

public class OwnerServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsAllOwners()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
          .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
          .Options;

        var context = new AppDbContext(options);


        context.Owners.AddRange(
            new Owner
            {
                Id = 1,
                Name = "John Doe",
                Email = "JohnDoe@example.com",
                Address = "JohnDoe Road",
                PhoneNumber = "07700 900000"
            },
            new Owner
            {
                Id = 2,
                Name = "Jane Doe",
                Email = "JaneDoe@example.com",
                Address = "JaneDoe Road",
                PhoneNumber = "07700 900001"
            }
        );

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var service = new OwnerService(context);

        var result = await service.GetAllAsync(CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

    }

    [Fact]
    public async Task GetByIdAsync_ReturnsOwner_WhenExists()
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

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var service = new OwnerService(context);

        var result = service.GetByIdAsync(1, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task CreateAsync_ReturnsOwner()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
           .Options;

        var context = new AppDbContext(options);

        var service = new OwnerService(context);

        var request = new CreateOwnerRequest
        {
            Name = "John Doe",
            Email = "JohnDoe@example.com",
            Address = "JohnDoe Road",
            PhoneNumber = "07700 900000"
        };

        var result = await service.CreateAsync(request, CancellationToken.None);

        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Email, result.Email);
        Assert.Equal(request.Address, result.Address);
        Assert.Equal(request.PhoneNumber, result.PhoneNumber);
    }


    [Fact]
    public async Task CreateAsync_ThrowsException_WhenEmailExists()
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

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var service = new OwnerService(context);

        var request = new CreateOwnerRequest
        {
            Name = "Jane Doe",
            Email = "JohnDoe@example.com",
            Address = "JaneDoe Road",
            PhoneNumber = "07700 900001"
        };

        var result = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(request, CancellationToken.None));

        Assert.Contains($"An owner with the email {request.Email} already exists.", result.Message);
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

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var service = new OwnerService(context);

        await service.DeleteAsync(1, CancellationToken.None);

        var result = await context.Owners.FindAsync([1], TestContext.Current.CancellationToken);

        Assert.Null(result);


    }

    [Fact]
    public async Task UpdateAsync_ReturnNewOwnerData()
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

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var service = new OwnerService(context);


        var request = new UpdateOwnerRequest
        {
            Name = "John Doe",
            Email = "JohnDoe12@example.com",
            Address = "Changed Road",
            PhoneNumber = "07700 900001"
        };


        await service.UpdateAsync(1, request, CancellationToken.None);

        var result = await context.Owners.FindAsync([1], TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Email, result.Email);
        Assert.Equal(request.Address, result.Address);
        Assert.Equal(request.PhoneNumber, result.PhoneNumber);

    }


    [Fact]
    public async Task UpdateAsync_ThrowsException_WhenEmailExists()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
              .UseInMemoryDatabase(databaseName: "TestDb" + Guid.NewGuid().ToString())
              .Options;

        var context = new AppDbContext(options);

        context.Owners.AddRange(new Owner
        {
            Id = 1,
            Name = "John Doe",
            Email = "JohnDoe@example.com",
            Address = "JohnDoe Road",
            PhoneNumber = "07700 900000"
        }, new Owner
        {

            Id = 2,
            Name = "Jane Doe",
            Email = "JaneDoe@example.com",
            Address = "JaneDoe Road",
            PhoneNumber = "07700 900001"
        }
        );

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var service = new OwnerService(context);


        var request = new UpdateOwnerRequest
        {
            Name = "John Doe",
            Email = "JaneDoe@example.com",
            Address = "JohnDoe Road",
            PhoneNumber = "07700 900000"
        };


        var result = await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateAsync(1, request, CancellationToken.None));

        Assert.Contains($"An owner with the email {request.Email} already exists.", result.Message);

    }


}

