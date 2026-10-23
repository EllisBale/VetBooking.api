using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;
namespace VetBooking.api.Services;


    public class AppointmentTypeService(AppDbContext context) : IAppointmentTypeService
    {
        public async Task<List<AppointmentTypeResponse>> GetAllAsync()
            => await context.AppointmentTypes
                .Select(a => new AppointmentTypeResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    DurationInMinutes = a.DurationInMinutes,
                    Price = a.Price
                })
                .ToListAsync();

        public async Task<AppointmentTypeResponse?> GetByIdAsync(int id)
            => await context.AppointmentTypes
                .Where(a => a.Id == id)
                .Select(a => new AppointmentTypeResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    DurationInMinutes = a.DurationInMinutes,
                    Price = a.Price
                }).FirstOrDefaultAsync();

    public async Task<AppointmentTypeResponse> CreateAsync(CreateAppointmentTypeRequest request)
    {
        var appointmentType = new AppointmentType
        {
            Name = request.Name,
            DurationInMinutes = request.DurationInMinutes,
            Price = request.Price
        };

        context.AppointmentTypes.Add(appointmentType);
        await context.SaveChangesAsync();

        return new AppointmentTypeResponse
        {
            Id = appointmentType.Id,
            Name = appointmentType.Name,
            DurationInMinutes = appointmentType.DurationInMinutes,
            Price = appointmentType.Price
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var appointmentType = await context.AppointmentTypes.FindAsync(id);

        if (appointmentType is null)
        {
            return false;
        }

        context.AppointmentTypes.Remove(appointmentType);
        await context.SaveChangesAsync();

        return true;
    }


    }

    