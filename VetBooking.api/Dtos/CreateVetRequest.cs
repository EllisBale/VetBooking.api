using System.ComponentModel.DataAnnotations;

namespace VetBooking.api.Dtos;

    public class CreateVetRequest
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Specialisation { get; set; } = string.Empty;
    }

