using VetBooking.api.Dtos;

namespace VetBooking.api.Services;

    public interface IVetScheduleService
    {
        Task<List<VetScheduleOverrideResponse>> GetAllOverridesAsync(int vetId, CancellationToken cancellationToken);
        Task<VetScheduleOverrideResponse?> GetOverrideByIdAsync(int id, CancellationToken cancellationToken);
        Task<VetScheduleOverrideResponse?> CreateOverrideAsync(CreateVetScheduleOverrideRequest request, CancellationToken cancellationToken);
        Task<bool> DeleteOverrideAsync(int id, CancellationToken cancellationToken);
        
    }

