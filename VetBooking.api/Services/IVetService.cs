using VetBooking.api.Dtos;
namespace VetBooking.api.Services;

    public interface IVetService
    {
        Task<List<VetResponse>> GetAllAsync();

        Task<VetResponse?> GetByIdAsync(int id);

        Task<VetResponse> CreateAsync(CreateVetRequest request);

        Task<bool> DeleteAsync(int id);

        Task<bool> UpdateAsync(int id, UpdateVetRequest request);

        Task<bool> ExistsAsync(int id);

        Task<List<WorkingHoursResponse>> GetWorkingHoursAsync(int vetId);

        Task<bool> SetWorkingHoursAsync(int vetId, DayOfWeek day, SetWorkingHoursRequest request);

    }

