using VetBooking.api.Dtos;

namespace VetBooking.api.Services;

    public interface IPetService
    {
        Task<List<PetResponse>> GetAllAsync(CancellationToken cancellationToken);

        Task<PetResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<PetResponse?> CreateAsync(CreatePetRequest request, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<bool> UpdateAsync(int id, UpdatePetRequest request, CancellationToken cancellationToken);
}

