using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;
namespace VetBooking.api.Services;


    public class AppointmentTypeService(AppDbContext context) : IAppointmentTypeService
    {
        public async Task<List<AppointmentTypeResponse>> GetAllAsync(CancellationToken cancellationToken)
            => await context.AppointmentTypes
                .Select(a => new AppointmentTypeResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    DurationInMinutes = a.DurationInMinutes,
                    Price = a.Price
                })
                .ToListAsync(cancellationToken);

        public async Task<AppointmentTypeResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
            => await context.AppointmentTypes
                .Where(a => a.Id == id)
                .Select(a => new AppointmentTypeResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    DurationInMinutes = a.DurationInMinutes,
                    Price = a.Price
                })
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<AppointmentTypeResponse> CreateAsync(CreateAppointmentTypeRequest request, CancellationToken cancellationToken)
        {
           
            var appointmentType = new AppointmentType
            {
                Name = request.Name,
                DurationInMinutes = request.DurationInMinutes,
                Price = request.Price
            };


            try
            {
                context.AppointmentTypes.Add(appointmentType);
                await context.SaveChangesAsync(cancellationToken);
            }

            catch (DbUpdateException ex)
            {
                throw new ArgumentException("Failed to create AppointmentType.", ex);
            }


            return new AppointmentTypeResponse
            {
                Id = appointmentType.Id,
                Name = appointmentType.Name,
                DurationInMinutes = appointmentType.DurationInMinutes,
                Price = appointmentType.Price
            };
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var appointmentType = await context.AppointmentTypes.FindAsync(id, cancellationToken);

            if (appointmentType is null)
            {
                return false;
            }

            try
            {
                context.AppointmentTypes.Remove(appointmentType);
                await context.SaveChangesAsync(cancellationToken);
            }

            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Could not delete this AppointmentType due to existing database constraints.", ex);
            }

            return true;
        }


        public async Task<bool> UpdateAsync(int id, UpdateAppointmentTypeRequest request, CancellationToken cancellationToken)
        {
            var appointmentType = await context.AppointmentTypes.FindAsync(id, cancellationToken);

            if (appointmentType is null)
            {
                return false;
            }

            appointmentType.Name = request.Name;
            appointmentType.DurationInMinutes = request.DurationInMinutes;
            appointmentType.Price = request.Price;

            try
            {
                await context.SaveChangesAsync(cancellationToken);
            }

            catch (DbUpdateException ex)
            {
                throw new ArgumentException("Failed to update AppointmentType.", ex);
            }
        
            return true;
        }


    }

    