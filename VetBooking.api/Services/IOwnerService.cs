using VetBooking.api.Dtos;
namespace VetBooking.api.Services;

    public interface IOwnerService
    {
        Task<List<OwnerResponse>> GetAllAsync(CancellationToken cancellationToken);

        Task<OwnerResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<OwnerResponse> CreateAsync(CreateOwnerRequest request, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<bool> UpdateAsync(int id, UpdateOwnerRequest request, CancellationToken cancellationToken);

    }

