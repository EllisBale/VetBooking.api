using VetBooking.api.Dtos;
namespace VetBooking.api.Services;

    public interface IVetService
    {
        Task<List<VetResponse>> GetAllAsync(CancellationToken cancellationToken);

        Task<VetResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<VetResponse> CreateAsync(CreateVetRequest request, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<bool> UpdateAsync(int id, UpdateVetRequest request, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(int id);

        Task<List<WorkingHoursResponse>> GetWorkingHoursAsync(int vetId, CancellationToken cancellationToken);

        Task<bool> SetWorkingHoursAsync(int vetId, DayOfWeek day, SetWorkingHoursRequest request, CancellationToken cancellationToken);

    }

