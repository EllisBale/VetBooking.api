namespace VetBooking.api.Dtos;

public class AppointmentResponse
{
    public int Id { get; set; }

    public DateTime StartUtc { get; set; }

    public DateTime EndUtc { get; set; }

    public AppointmentStatusResponse Status { get; set; } = new AppointmentStatusResponse();

    public string? Notes { get; set; } 

    public PetResponse Pet { get; set; } = null!;

    public VetResponse Vet { get; set; } = null!;

}


