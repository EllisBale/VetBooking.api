using VetBooking.api.Dtos;

namespace VetBooking.api.Services;

    public interface IAppointmentService
    {
        Task<List<AppointmentDetailResponse>> GetAllAsync(CancellationToken cancellationToken);

        Task<AppointmentDetailResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<AppointmentDetailResponse?> CreateAsync(CreateAppointmentRequest request, CancellationToken cancellationToken);

        Task<bool> HasAppointmentOverlapAsync(int vetId, int petId, DateTime requestedStart, DateTime requestedEnd);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

    }

