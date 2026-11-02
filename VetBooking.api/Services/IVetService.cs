using VetBooking.api.Dtos;
namespace VetBooking.api.Services;

    public interface IVetService
    {
        Task<List<VetResponse>> GetAllAsync();

        Task<VetResponse?> GetByIdAsync(int id);

    }

