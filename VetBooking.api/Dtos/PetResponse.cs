namespace VetBooking.api.Dtos;

    public class PetResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public int OwnerId { get; set; }

    }

