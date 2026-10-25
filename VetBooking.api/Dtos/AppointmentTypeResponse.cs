using System.ComponentModel.DataAnnotations;
namespace VetBooking.api.Dtos;

    public class AppointmentTypeResponse
    {

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
    
        [Range(1, 480)]
        public int DurationInMinutes { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }
    }

