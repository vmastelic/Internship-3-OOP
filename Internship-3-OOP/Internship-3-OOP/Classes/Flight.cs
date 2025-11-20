namespace Internship_3_OOP.Classes
{
        public enum TicketType
    {
        Standard,
        Business,
        VIP
    }
    public class Flight : BaseClass
    {
        public Flight() { }
        public string Name { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public int Distance { get; set; }
        public TimeSpan Duration { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }

        public Airplane Airplane { get; set; }

        public Crew Crew { get; set; }
        public List<Passenger> Passengers { get; set; } = new List<Passenger>();
        public Flight(string name, string start, string end, DateTime departure, DateTime arrival, int distance, TimeSpan duration)
        {
            Name = name;
            StartLocation = start;
            EndLocation = end;
            Departure = departure;
            Arrival = arrival;
            Distance = distance;
            Duration = duration;

        }
        public int GetAvailableSeats(TicketType type)
        {
            int usedSeats = Passengers.SelectMany(passenger => passenger.Reservations).Count(reservation => reservation.Flight == this && reservation.TicketType == type);

            return type switch
            {
                TicketType.Standard => Airplane.StandardCapacity - usedSeats,
                TicketType.Business => Airplane.BusinessCapacity - usedSeats,
                TicketType.VIP => Airplane.VipCapacity - usedSeats,
                _ => 0
            };
        }
    }
}
