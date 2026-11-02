using VetBooking.api.Models;

namespace VetBooking.api.Dtos;

public class AppointmentStatusResponse
{
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Booked;
}

