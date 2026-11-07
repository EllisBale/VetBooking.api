namespace VetBooking.api.Dtos;

public class WorkingHoursResponse
{
    public DayOfWeek DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

}

