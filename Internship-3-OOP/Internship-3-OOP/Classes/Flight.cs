namespace Internship_3_OOP.Classes
{
    public class Flight
    {
        public string Name { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public int Distance { get; set; }
        public TimeSpan Duration { get; set; }

        public List<Passenger> Passengers { get; set; } = new List<Passenger>();
    }
}
