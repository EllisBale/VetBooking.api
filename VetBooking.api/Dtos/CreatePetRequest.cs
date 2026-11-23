using System.ComponentModel;
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

        [DefaultValue(false)]
        public bool IsDeceased { get; set; } = false;

        public DateTime? DeceasedDate { get; set; }

        public int? OwnerId { get; set; }
    }

