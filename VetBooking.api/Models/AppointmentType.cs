namespace VetBooking.api.Models
{
    public class AppointmentType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }

        public decimal Price { get; set; }

        public List<Appointment> Appointments { get; set; } = new();
    }
}
