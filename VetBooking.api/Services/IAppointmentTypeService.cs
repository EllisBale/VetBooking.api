using VetBooking.api.Dtos;

namespace VetBooking.api.Services;

    public interface IAppointmentTypeService
    {
        Task<List<AppointmentTypeResponse>> GetAllAsync();

        Task<AppointmentTypeResponse?> GetByIdAsync(int id);

        Task<AppointmentTypeResponse> CreateAsync(CreateAppointmentTypeRequest request);

        Task<bool> DeleteAsync(int id);

        Task<bool> UpdateAsync(int id, UpdateAppointmentTypeRequest request);

}
