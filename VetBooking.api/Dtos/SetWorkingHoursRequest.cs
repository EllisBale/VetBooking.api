namespace VetBooking.api.Dtos;

public class SetWorkingHoursRequest
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}

