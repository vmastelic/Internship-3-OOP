namespace Internship_3_OOP.Classes
{
    internal class InitialData
    {
        public static List<Passenger> Passengers = new List<Passenger>();
        public static List<Flight> Flights = new List<Flight>();

        public static void Initialize()
        {
            Passengers.Add(new Passenger
            {
                Name = "Ivan",
                Surname = "Ivić",
                Email = "ivan@gmail.com",
                Password = "1234",
                BirthDate = new DateOnly(2000, 10, 10)
            });

            Passengers.Add(new Passenger
            {
                Name = "Ana",
                Surname = "Anić",
                Email = "ana@gmail.com",
                Password = "4321",
                BirthDate = new DateOnly(1995, 8, 21)
            });
            
            Flights.Add(new Flight
            {
                Name = "Pariz",
                Departure = new DateTime(2025, 6, 10, 14, 30, 00),
                Arrival = new DateTime(2025, 6, 10, 16, 45, 00),
                Distance = 550,
                Duration = TimeSpan.FromHours(2.25)
            });

            Flights.Add(new Flight
            {
                Name = "Rim",
                Departure = new DateTime(2025, 6, 12, 8, 0, 00),
                Arrival = new DateTime(2025, 6, 12, 9, 30, 00),
                Distance = 650,
                Duration = TimeSpan.FromHours(1.5)
            });
        }
    }
}
