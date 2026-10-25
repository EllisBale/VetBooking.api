using System.ComponentModel.DataAnnotations;
namespace VetBooking.api.Dtos;

    public class UpdateAppointmentTypeRequest
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 400)]
        public int DurationInMinutes { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }
    }

