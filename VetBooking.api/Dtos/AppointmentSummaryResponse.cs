using VetBooking.api.Models;

namespace VetBooking.api.Dtos;

public class AppointmentSummaryResponse
{
    public int Id { get; set; }

    public DateTime StartUtc { get; set; }
    public DateTime EndUtc { get; set; }

    public AppointmentStatus Status { get; set; }
    public string AppointmentTypeName { get; set; } = string.Empty;

    public string PetName { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public string VetName { get; set; } = string.Empty;

    public string? ExamRoom { get; set; }

}


