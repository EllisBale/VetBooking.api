using VetBooking.api.Dtos;
namespace VetBooking.api.Services;

    public interface IOwnerService
    {
        public Task<List<OwnerResponse>> GetAllAsync();
    }

