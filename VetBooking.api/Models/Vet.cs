namespace VetBooking.api.Models
{
    public class Vet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialisation { get; set; } = string.Empty;

        public List<VetWorkingHours> WorkingHours { get; set; } = new();
        public List<Appointment> Appointments { get; set; } = new();
    }
}
