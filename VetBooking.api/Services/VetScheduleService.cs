using Microsoft.EntityFrameworkCore;
using VetBooking.api.Data;
using VetBooking.api.Dtos;
using VetBooking.api.Models;

namespace VetBooking.api.Services;

    public class VetScheduleService(AppDbContext context) : IVetScheduleService
    {
        
        public async Task<List<VetScheduleOverrideResponse>> GetAllOverridesAsync(int vetId, CancellationToken cancellationToken)
        {
            return await context.VetScheduleOverrides
                .Where(o => o.VetId == vetId)
                .Select(o => new VetScheduleOverrideResponse
                {
                    Id = o.Id,
                    VetId = o.VetId,
                    Date = o.Date,
                    StartTime = o.StartTime,
                    EndTime = o.EndTime,
                    IsAvailable = o.IsAvailable
                })
                .ToListAsync(cancellationToken);
        }


        public async Task<VetScheduleOverrideResponse?> GetOverrideByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context.VetScheduleOverrides
                .Where(o => o.Id == id)
                .Select(o => new VetScheduleOverrideResponse
                {
                    Id = o.Id,
                    VetId = o.VetId,
                    Date = o.Date,
                    StartTime = o.StartTime,
                    EndTime = o.EndTime,
                    IsAvailable = o.IsAvailable
                })
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<VetScheduleOverrideResponse?> CreateOverrideAsync(CreateVetScheduleOverrideRequest request, CancellationToken cancellationToken)
        {

            if (!await context.Vets.AnyAsync(v => v.Id == request.VetId, cancellationToken))
            {
                throw new ArgumentException($"Vet with ID {request.VetId} does not exist.");
            }

            var overrideEntity = new VetScheduleOverride
            {
               VetId = request.VetId,
               Date = request.Date,
               StartTime = request.StartTime,
               EndTime = request.EndTime,
               IsAvailable = request.IsAvailable,
               Reason = request.Reason
            };


            try
            {
                context.VetScheduleOverrides.Add(overrideEntity);
                await context.SaveChangesAsync(cancellationToken);
            }

            catch (DbUpdateException ex)
            {
                throw new ArgumentException("An error occurred while creating Vet Schedule Override.", ex);
            }

            return new VetScheduleOverrideResponse
            {
                VetId = overrideEntity.VetId,
                Date = overrideEntity.Date,
                StartTime = overrideEntity.StartTime,
                EndTime = overrideEntity.EndTime,
                IsAvailable = overrideEntity.IsAvailable,
                Reason = overrideEntity.Reason
            };
        }


        public async Task<bool> DeleteOverrideAsync(int id, CancellationToken cancellationToken)
        {
            var overrideEntity = await context.VetScheduleOverrides.FindAsync(id , cancellationToken);

            if (overrideEntity is null)
            {
                return false;
            }

            try
            {
                context.VetScheduleOverrides.Remove(overrideEntity);
                await context.SaveChangesAsync(cancellationToken);
                
            }

            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while deleting Vet Schedule Override.", ex);
            }

            return true;
        }

    }

