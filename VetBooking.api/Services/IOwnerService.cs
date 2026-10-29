using VetBooking.api.Dtos;
namespace VetBooking.api.Services;

    public interface IOwnerService
    {
        Task<List<OwnerResponse>> GetAllAsync();

        Task<OwnerResponse?> GetByIdAsync(int id);

        Task<OwnerResponse> CreateAsync(CreateOwnerRequest request);

        Task<bool> DeleteAsync(int id);

        Task<bool> UpdateAsync(int id, UpdateOwnerRequest request);


}

