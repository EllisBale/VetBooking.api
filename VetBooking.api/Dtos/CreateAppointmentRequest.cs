using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using VetBooking.api.Models;

namespace VetBooking.api.Dtos;

    public class CreateAppointmentRequest
    {
        [Required]
        public DateTime StartUtc { get; set; }

        [Required]
        public DateTime EndUtc { get; set; }

        [Required]
        [EnumDataType(typeof(AppointmentStatus), ErrorMessage = "The selected status value is invalid.")]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Booked;

        [StringLength(256)]
        public string? Notes { get; set; }

        [Required]
        public int? PetId { get; set; }

        [Required]
        public int? VetId { get; set; }

        [Required]
        public int? AppointmentTypeId { get; set; }
    }

