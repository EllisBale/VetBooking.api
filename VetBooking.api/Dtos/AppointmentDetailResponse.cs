using VetBooking.api.Models;

namespace VetBooking.api.Dtos;

    public class AppointmentDetailResponse
    {
        public int Id { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public AppointmentStatus Status { get; set; }
        public string? Notes { get; set; }
        public string? ExamRoom { get; set; }

        public AppointmentTypeResponse AppointmentType { get; set; } = null!;
        public PetResponse Pet { get; set; } = null!;
        public VetResponse Vet { get; set; } = null!;

        public int OwnerId { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string OwnerPhoneNumber { get; set; } = string.Empty;
        public string OwnerEmail { get; set; } = string.Empty;
    }

