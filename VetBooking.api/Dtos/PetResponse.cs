namespace VetBooking.api.Dtos;

    public class PetResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public bool IsDeceased { get; set; } = false;

        public DateTime? DeceasedDate { get; set; }

        public int OwnerId { get; set; }

        public OwnerResponse? Owner { get; set; }

    }

