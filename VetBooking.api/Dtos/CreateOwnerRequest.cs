using System.ComponentModel.DataAnnotations;

namespace VetBooking.api.Dtos;

    public class CreateOwnerRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
    
        [RegularExpression(@"^\+?[0-9 ]{10,15}$", 
            ErrorMessage = "Phone number must contain only digits, spaces and an optional leading +, and be 10 to 15 characters long.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }

