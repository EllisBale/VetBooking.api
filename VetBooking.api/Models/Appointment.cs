namespace VetBooking.api.Models;

    public class Appointment
    {
        public int Id { get; set; }
        public DateTime StartUtc { get; set; }

        public DateTime EndUtc { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Booked;

        public string? Notes { get; set; }

        public Pet Pet { get; set; } = null!;

        public int VetId { get; set; }
        public Vet Vet { get; set; } = null!;

        public int AppointmentTypeId { get; set; }
    }

