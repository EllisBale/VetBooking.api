using VetBooking.api.Dtos;

namespace VetBooking.api.Services;

    public interface IPetService
    {
        Task<List<PetResponse>> GetAllAsync();

        Task<PetResponse?> GetByIdAsync(int id);

        Task<PetResponse?> CreateAsync(CreatePetRequest request);

        Task<bool> DeleteAsync(int id);

        Task<bool> UpdateAsync(int id, UpdatePetRequest request);
}

