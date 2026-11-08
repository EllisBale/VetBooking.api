using VetBooking.api.Dtos;

namespace VetBooking.api.Services;

    public interface IAppointmentService
    {
        Task<List<AppointmentResponse>> GetAllAsync();

        Task<AppointmentResponse?> GetByIdAsync(int id);

        Task<AppointmentResponse?> CreateAsync(CreateAppointmentRequest request);

        Task<bool> HasAppointmentOverlapAsync(int vetId, DateTime requestedStart, DateTime requestedEnd);

    }

