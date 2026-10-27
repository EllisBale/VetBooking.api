using VetBooking.api.Dtos;
namespace VetBooking.api.Services;

    public interface IOwnerService
    {
        public Task<List<OwnerResponse>> GetAllAsync();

        public Task<OwnerResponse?> GetByIdAsync(int id);

        public Task<OwnerResponse> CreateAsync(CreateOwnerRequest request);
    }

