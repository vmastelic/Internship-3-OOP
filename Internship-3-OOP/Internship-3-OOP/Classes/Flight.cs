namespace Internship_3_OOP.Classes
{
    public class Flight : BaseClass
    {
        public string Name { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public int Distance { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Passenger> Passengers { get; set; } = new List<Passenger>();
        public TicketType TicketType { get; set; }

        public Flight(string name, DateTime departure, DateTime arrival, int distance, TimeSpan duration)
        {
            Name = name;
            Departure = departure;
            Arrival = arrival;
            Distance = distance;
            Duration = duration;
        }

        public void AddPassenger(Passenger passenger)
        {
            if (!Passengers.Contains(passenger))
            {
                Passengers.Add(passenger);
                passenger.ReservedFlights.Add(this);
            }
        }

    }
}
