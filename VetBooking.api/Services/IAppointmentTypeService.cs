using VetBooking.api.Dtos;

namespace VetBooking.api.Services;

    public interface IAppointmentTypeService
    {
        Task<List<AppointmentTypeResponse>> GetAllAsync(CancellationToken cancellationToken);

        Task<AppointmentTypeResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<AppointmentTypeResponse> CreateAsync(CreateAppointmentTypeRequest request, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<bool> UpdateAsync(int id, UpdateAppointmentTypeRequest request, CancellationToken cancellationToken);

}
