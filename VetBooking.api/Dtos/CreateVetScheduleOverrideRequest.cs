using System.ComponentModel.DataAnnotations;

namespace VetBooking.api.Dtos;

    public class CreateVetScheduleOverrideRequest
    {
        [Required]
        public int VetId { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required]
        [StringLength(200)]
        public string? Reason { get; set; } = null!;
    }

