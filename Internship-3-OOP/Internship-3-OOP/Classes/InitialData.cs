namespace Internship_3_OOP.Classes
{
    internal class InitialData
    {
        public static List<Passenger> Passengers = new List<Passenger>();
        public static List<Flight> Flights = new List<Flight>();
        public static List<Crew> Crews = new List<Crew>();
        public static List<Airplane> Airplanes = new List<Airplane>();

        public static void Initialize()
        {
            var firstPassenger = new Passenger("Ivan", "Ivić", new DateOnly(2000, 10, 10), "ivan@gmail.com", "1234");
            var secondPassenger = new Passenger("Ana", "Anić", new DateOnly(1995, 8, 21), "ana@gmail.com", "4321");

            Passengers.Add(firstPassenger);
            Passengers.Add(secondPassenger);
            
            var firstFlight = new Flight("Pariz", "Split", "Pariz", new DateTime(2025, 6, 10, 14, 30, 0), new DateTime(2025, 6, 10, 16, 45, 0), 550, TimeSpan.FromHours(2.25), 100, 20, 10);
            var secondFlight = new Flight("Rim", "Split", "Rim", new DateTime(2025, 6, 12, 8, 0, 0), new DateTime(2025, 6, 12, 9, 30, 0), 650, TimeSpan.FromHours(2.25), 100, 20, 10);
            var thirdFlight = new Flight("Berlin", "Split", "Berlin", new DateTime(2025, 6, 12, 8, 0, 0), new DateTime(2025, 6, 12, 9, 30, 0), 650, TimeSpan.FromHours(2.25), 100, 20, 10);

            var firstReservation = new Reservation(firstPassenger, firstFlight, TicketType.Standard);
            var secondReservation = new Reservation(secondPassenger, secondFlight, TicketType.Standard);
            var thirdReservation = new Reservation(secondPassenger, thirdFlight, TicketType.Business);

            Flights.Add(firstFlight);
            Flights.Add(secondFlight);
            Flights.Add(thirdFlight);

            firstPassenger.AddReservation(firstReservation);
            secondPassenger.AddReservation(secondReservation);
            secondPassenger.AddReservation(thirdReservation);

            var firstPilot = new CrewMember("Ivica", "Ivišić", Gender.Male, Position.Pilot, new DateOnly(1975, 3, 2));
            var firstCopilot = new CrewMember("Jurica", "Ivišić", Gender.Male, Position.Copilot, new DateOnly(1977, 3, 2));
            var firstStewardess1 = new CrewMember("Josipa", "Josipović", Gender.Female, Position.Stewardess, new DateOnly(1990, 3, 2));
            var firstStewardess2 = new CrewMember("Anđela", "Josipović", Gender.Female, Position.Stewardess, new DateOnly(1995, 3, 2));

            var secondPilot = new CrewMember("Petar", "Perić", Gender.Male, Position.Pilot, new DateOnly(1975, 3, 2));
            var secondCopilot = new CrewMember("Marko", "Markić", Gender.Male, Position.Copilot, new DateOnly(1977, 3, 2));
            var secondStewardess1 = new CrewMember("Ana", "Ančić", Gender.Female, Position.Stewardess, new DateOnly(1990, 3, 2));
            var secondStewardess2 = new CrewMember("Petra", "Perić", Gender.Female, Position.Stewardess, new DateOnly(1995, 3, 2));

            Crews.Add(new Crew("Prva Posada", firstPilot, firstCopilot, firstStewardess1, firstStewardess2));
            Crews.Add(new Crew("Druga Posada", secondPilot, secondCopilot, secondStewardess1, secondStewardess2));

            firstFlight.Crew = Crews[0];
            firstFlight.Crew.IsAvailable = false;

            var firstPlane = new Airplane("Boeing 747", 1980);
            var secondPlane = new Airplane("Boeing 737", 1990);

            Airplanes.Add(firstPlane);
            Airplanes.Add(secondPlane);

            firstFlight.Airplane = firstPlane;
            secondFlight.Airplane = secondPlane;
            thirdFlight.Airplane = secondPlane;
        }
        
        public static void PrintAllFlights()
        {
            Console.WriteLine("Popis svih letova: ");
            foreach (var flight in Flights)
                Console.WriteLine($"{flight.ID} - {flight.Name} - {flight.Departure:yyyy-MM-dd} - {flight.Arrival:yyyy-MM-dd} - {flight.Distance}km - {flight.Duration}");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }

        public static void FindFlight()
        {
            Console.Clear();
            Console.WriteLine("Pretrazi let po\na) ID\nb) Ime");
            Console.Write("\nOdabir: ");
            var choice = Console.ReadLine();

            if (choice == "a")
            {
                Console.Write("\nUnesi ID: ");
                if (!int.TryParse(Console.ReadLine(), out int flightID))
                {
                    Console.WriteLine("Neispravan ID!");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                var wantedFlight = Flights.FirstOrDefault(flight => flight.ID == flightID);

                if (wantedFlight == null)
                    Console.WriteLine("Let s tim ID-om ne postoji.");
                else
                {
                    Console.WriteLine("Traženi let: ");
                    Console.WriteLine($"{wantedFlight.ID} - {wantedFlight.Name} - {wantedFlight.StartLocation} -> {wantedFlight.EndLocation} - {wantedFlight.Departure:yyyy-MM-dd HH:mm} - {wantedFlight.Arrival:yyyy-MM-dd HH:mm} - {wantedFlight.Distance}km - {wantedFlight.Duration}");
                    if (wantedFlight.Crew != null)
                    {
                        Console.WriteLine($"Posada: {wantedFlight.Crew.Name}");
                        Console.WriteLine($"Pilot: {wantedFlight.Crew.Pilot.Name} {wantedFlight.Crew.Pilot.Surname}");
                        Console.WriteLine($"Kopilot: {wantedFlight.Crew.Copilot.Name} {wantedFlight.Crew.Copilot.Surname}");
                        Console.WriteLine($"Stjuardesa 1: {wantedFlight.Crew.Stewardess1.Name} {wantedFlight.Crew.Stewardess1.Surname}");
                        Console.WriteLine($"Stjuardesa 2: {wantedFlight.Crew.Stewardess2.Name} {wantedFlight.Crew.Stewardess2.Surname}");
                    }
                    else
                    {
                        Console.WriteLine("Posada nije dodijeljena ovom letu.");
                    }
                    Console.WriteLine($"Avion: {wantedFlight.Airplane.Name}\n Godina proizvodnje: {wantedFlight.Airplane.Year}");
                }
            }
            else if (choice == "b")
            {
                Console.Write("\nUnesi ime: ");
                var flightName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(flightName))
                {
                    Console.WriteLine("Neispravan unos imena");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                var wantedFlight = Flights.FirstOrDefault(flight => flight.Name.Equals(flightName, StringComparison.OrdinalIgnoreCase));
                if (wantedFlight == null)
                    Console.WriteLine("Let s tim imenom ne postoji.");
                else
                {
                    Console.WriteLine("Traženi let: ");
                    Console.WriteLine($"{wantedFlight.ID} - {wantedFlight.Name} - {wantedFlight.StartLocation} -> {wantedFlight.EndLocation} - {wantedFlight.Departure:yyyy-MM-dd HH:mm} - {wantedFlight.Arrival:yyyy-MM-dd HH:mm} - {wantedFlight.Distance}km - {wantedFlight.Duration}");
                    if (wantedFlight.Crew != null)
                    {
                        Console.WriteLine($"Posada: {wantedFlight.Crew.Name}");
                        Console.WriteLine($"Pilot: {wantedFlight.Crew.Pilot.Name} {wantedFlight.Crew.Pilot.Surname}");
                        Console.WriteLine($"Kopilot: {wantedFlight.Crew.Copilot.Name} {wantedFlight.Crew.Copilot.Surname}");
                        Console.WriteLine($"Stjuardesa 1: {wantedFlight.Crew.Stewardess1.Name} {wantedFlight.Crew.Stewardess1.Surname}");
                        Console.WriteLine($"Stjuardesa 2: {wantedFlight.Crew.Stewardess2.Name} {wantedFlight.Crew.Stewardess2.Surname}");
                    }
                    else
                    {
                        Console.WriteLine("Posada nije dodijeljena ovom letu.");
                    }
                    Console.WriteLine($"Avion: {wantedFlight.Airplane.Name}\n Godina proizvodnje: {wantedFlight.Airplane.Year}");
                }
            }

            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
    }
}