namespace VetBooking.api.Dtos;

    public class AppointmentTypeResponse
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }
    }

