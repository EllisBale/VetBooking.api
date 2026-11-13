using VetBooking.api.Dtos;

namespace VetBooking.api.Services;

    public interface IAppointmentService
    {
        Task<List<AppointmentResponse>> GetAllAsync(CancellationToken cancellationToken);

        Task<AppointmentResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<AppointmentResponse?> CreateAsync(CreateAppointmentRequest request, CancellationToken cancellationToken);

        Task<bool> HasAppointmentOverlapAsync(int vetId, DateTime requestedStart, DateTime requestedEnd);

    }

