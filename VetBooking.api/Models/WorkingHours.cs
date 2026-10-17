namespace VetBooking.api.Models;

    public class WorkingHours
    {
        public int Id { get; set; }
       
        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }


        public int VetId { get; set; }
        public Vet Vet { get; set; } = null!;

    }

