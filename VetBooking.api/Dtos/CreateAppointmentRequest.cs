using System.ComponentModel.DataAnnotations;

namespace VetBooking.api.Dtos;

    public class CreateAppointmentRequest
    {
        [Required]
        public DateTime StartUtc { get; set; }

        [Required]
        public DateTime EndUtc { get; set; }

        [Required]
        public AppointmentStatusResponse Status { get; set; } = new AppointmentStatusResponse();

        [StringLength(256)]
        public string? Notes { get; set; }

        [Required]
        public int PetId { get; set; }

        [Required]
        public int VetId { get; set; }

        [Required]
        public int AppointmentTypeId { get; set; }
    }

