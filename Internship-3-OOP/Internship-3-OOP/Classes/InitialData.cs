namespace Internship_3_OOP.Classes
{
    internal class InitialData
    {
        public static List<Passenger> Passengers = new List<Passenger>();
        public static List<Flight> Flights = new List<Flight>();

        public static void Initialize()
        {
            var firstPassenger = new Passenger("Ivan", "Ivić", new DateOnly(2000, 10, 10), "ivan@gmail.com", "1234");
            var secondPassenger = new Passenger("Ana", "Anić", new DateOnly(1995, 8, 21), "ana@gmail.com", "4321");

            Passengers.Add(firstPassenger);
            Passengers.Add(secondPassenger);

            var firstFlight = new Flight("Pariz", new DateTime(2025, 6, 10, 14, 30, 0), new DateTime(2025, 6, 10, 16, 45, 0), 550, TimeSpan.FromHours(2.25), 100, 20, 10);
            var secondFlight = new Flight("Rim", new DateTime(2025, 6, 12, 8, 0, 0), new DateTime(2025, 6, 12, 9, 30, 0), 650, TimeSpan.FromHours(2.25), 100, 20, 10);
            var thirdFlight = new Flight("Berlin", new DateTime(2025, 6, 12, 8, 0, 0), new DateTime(2025, 6, 12, 9, 30, 0), 650, TimeSpan.FromHours(2.25), 100, 20, 10);

            var firstReservation = new Reservation(firstPassenger, firstFlight, TicketType.Standard);
            var secondReservation = new Reservation(secondPassenger, secondFlight, TicketType.Standard);
            var thirdReservation = new Reservation(secondPassenger, thirdFlight, TicketType.Business);

            Flights.Add(firstFlight);
            Flights.Add(secondFlight);
            Flights.Add(thirdFlight);

            firstPassenger.AddReservation(firstReservation);
            secondPassenger.AddReservation(secondReservation);
            secondPassenger.AddReservation(thirdReservation);
        }
    }
}