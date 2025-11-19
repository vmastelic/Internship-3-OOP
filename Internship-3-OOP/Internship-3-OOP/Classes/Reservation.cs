namespace Internship_3_OOP.Classes
{
    public class Reservation : BaseClass
    {
        public Passenger Passenger { get; set; }
        public Flight Flight { get; set; }
        public TicketType TicketType { get; set; }
        public DateTime ReservedAt { get; set; } = DateTime.Now;
        public Reservation(Passenger passenger, Flight flight, TicketType ticketType)
        {
            Passenger = passenger;
            Flight = flight;
            TicketType = ticketType;
        }
    }
}
