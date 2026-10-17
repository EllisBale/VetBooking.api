namespace VetBooking.api.Models;

    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public int OwnerId { get; set; }

        public Owner Owner { get; set; } = null!;

        public List<Appointment> Appointments { get; set; } = new();
    }

