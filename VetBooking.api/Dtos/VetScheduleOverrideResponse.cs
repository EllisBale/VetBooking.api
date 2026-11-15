namespace VetBooking.api.Dtos;

    public class VetScheduleOverrideResponse
    {
        public int Id { get; set; }
        public int VetId { get; set; }
        public bool IsAvailable { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string? Reason { get; set; }
    }

