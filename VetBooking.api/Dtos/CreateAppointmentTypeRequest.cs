namespace VetBooking.api.Dtos;

    public class CreateAppointmentTypeRequest
    {
        public string Name { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }
    }

