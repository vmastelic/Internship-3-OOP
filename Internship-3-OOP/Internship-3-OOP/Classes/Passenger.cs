namespace Internship_3_OOP.Classes
{
    public class Passenger : BaseClass
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Flight> ReservedFlights { get; set; } = new List<Flight>();

        public Passenger() { }
        public Passenger(string name, string surname, DateOnly birthDate,
                     string email, string password)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            Email = email;
            Password = password;
        }
        public void AddFlight(Flight flight)
        {
            if (!ReservedFlights.Contains(flight))
            {
                ReservedFlights.Add(flight);
                flight.Passengers.Add(this);
            }
        }

        public void PrintReservedFlights(Passenger passenger)
        {
            Console.Clear();
            foreach (var flight in ReservedFlights)
                Console.WriteLine($"{flight.ID} - {flight.Name} - {flight.Departure.ToString("yyyy-MM-dd")} - {flight.Arrival.ToString("yyyy-MM-dd")} - {flight.Distance} - {flight.Duration}");

            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadLine();
        }

        public void ReserveFlight(Passenger passenger)
        {
            Console.Clear();
            var availableFlights = InitialData.Flights.Where(flight => !passenger.ReservedFlights.Contains(flight)).ToList();

            if (availableFlights.Count == 0)
            {
                Console.WriteLine("Nema dostupnih letova za rezervaciju.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Dostupni letovi: ");
            foreach(var flight in availableFlights)
                Console.WriteLine($"{flight.ID} - {flight.Name} - {flight.Departure:yyyy-MM-dd} - {flight.Arrival:yyyy-MM-dd} - {flight.Distance}km - {flight.Duration}");

            Console.Write("\nUnesite ID leta koji zelite rezervirati: ");
            if(!int.TryParse(Console.ReadLine(), out int flightID))
            {
                Console.WriteLine("Neispravan ID!");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }

            var selectedFlight = availableFlights.FirstOrDefault(flight => flight.ID == flightID);
            if (selectedFlight == null)
            {
                Console.WriteLine("Let s tim ID-om ne postoji ili je već rezerviran.");
                Console.ReadKey();
                return;
            }
            passenger.ReservedFlights.Add(selectedFlight);
            selectedFlight.Passengers.Add(passenger);
            Console.WriteLine($"\nLet '{selectedFlight.Name}' uspješno rezerviran!");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadLine();
        }

    }
}
