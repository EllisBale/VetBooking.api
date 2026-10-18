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
            new Vet { Id = 1, Name = "Test", Specialisation = "Surgery" }
            
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
    }




}

 