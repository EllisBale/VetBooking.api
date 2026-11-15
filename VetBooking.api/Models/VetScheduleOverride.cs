namespace VetBooking.api.Models;

    public class VetScheduleOverride
    {
        public int Id { get; set; }
        public int VetId { get; set; }

        public Vet Vet { get; set; } = null!;


        public bool IsAvailable { get; set; }

        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public string? Reason { get; set; }

    }

