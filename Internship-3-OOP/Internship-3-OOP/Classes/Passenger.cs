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

    }
}
