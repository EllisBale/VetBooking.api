namespace VetBooking.api.Data;
using Microsoft.EntityFrameworkCore;
using VetBooking.api.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<Vet> Vets => Set<Vet>();
    public DbSet<AppointmentType> AppointmentTypes => Set<AppointmentType>();
    public DbSet<WorkingHours> VetWorkingHours => Set<WorkingHours>();
    public DbSet<Appointment> Appointments => Set<Appointment>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)     {
       
        modelBuilder.Entity<Vet>().HasData(
            new Vet { 
                Id = 1, Name = "Test",
                Specialisation = "Surgery",
                Email = "Vet@example.com",
                PhoneNumber = "07700 900000"
            }
            
        );

        modelBuilder.Entity<WorkingHours>().HasData(

            new WorkingHours
            {
                Id = 1,
                VetId = 1,
                DayOfWeek = DayOfWeek.Monday,
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(17, 0)
            },

            new WorkingHours
            {
                Id = 2,
                VetId = 1,
                DayOfWeek = DayOfWeek.Tuesday,
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(17, 0)
            }
        );

        modelBuilder.Entity<AppointmentType>().HasData(
            new AppointmentType { Id = 1, Name = "Checkup", DurationInMinutes = 30, Price = 50.00m },
            new AppointmentType { Id = 2, Name = "Vaccination", DurationInMinutes = 15, Price = 30.00m },
            new AppointmentType { Id = 3, Name = "Surgery", DurationInMinutes = 120, Price = 500.00m },
            new AppointmentType { Id = 4, Name = "Dental Cleaning", DurationInMinutes = 60, Price = 150.00m },
            new AppointmentType { Id = 5, Name = "Emergency", DurationInMinutes = 45, Price = 200.00m }
        );


        modelBuilder.Entity<Vet>()
            .HasIndex(v => v.Email)
            .IsUnique();

        modelBuilder.Entity<Vet>()
            .HasIndex(v => v.PhoneNumber)
            .IsUnique();

        modelBuilder.Entity<Owner>()
            .HasIndex(o => o.Email)
            .IsUnique();

        modelBuilder.Entity<Owner>()
            .HasIndex(o => o.PhoneNumber);


    }




}

 