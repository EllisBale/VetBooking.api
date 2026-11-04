using System.ComponentModel.DataAnnotations;


namespace VetBooking.api.Dtos;

    public class CreatePetRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Species { get; set; } = string.Empty;


        [StringLength(200)]
        public string Breed { get; set; } = string.Empty;

        [Required]
        public DateOnly DateOfBirth { get; set; }
        public int? OwnerId { get; set; }
    }

