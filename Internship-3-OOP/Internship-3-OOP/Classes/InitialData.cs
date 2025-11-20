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
            
            var firstFlight = new Flight("Pariz", "Split", "Pariz", new DateTime(2025, 6, 10, 14, 30, 0), new DateTime(2025, 6, 10, 16, 45, 0), 550, TimeSpan.FromHours(2.25));
            var secondFlight = new Flight("Rim", "Split", "Rim", new DateTime(2025, 6, 12, 8, 0, 0), new DateTime(2025, 6, 12, 9, 30, 0), 650, TimeSpan.FromHours(2.25));
            var thirdFlight = new Flight("Berlin", "Split", "Berlin", new DateTime(2025, 6, 12, 8, 0, 0), new DateTime(2025, 6, 12, 9, 30, 0), 650, TimeSpan.FromHours(2.25));

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

            var firstPlane = new Airplane("Boeing 747", 1980, 100, 20, 10);
            var secondPlane = new Airplane("Boeing 737", 1990, 100, 20, 10);

            Airplanes.Add(firstPlane);
            Airplanes.Add(secondPlane);

            firstFlight.Airplane = firstPlane;
            firstPlane.FlightsNumber++;
            secondFlight.Airplane = secondPlane;
            secondPlane.FlightsNumber++;
            thirdFlight.Airplane = secondPlane;
            secondPlane.FlightsNumber++;

        }
        public static void PrintAllFlights()
        {
            Console.Clear();
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
        public static void EditFlight()
        {
            Console.Write("\nUnesi ID leta koji želiš urediti: ");
            if (!int.TryParse(Console.ReadLine(), out int flightID))
            {
                Console.WriteLine("Neispravan unos ID-ja!");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }
            var wantedFlight = Flights.FirstOrDefault(flight => flight.ID == flightID);

            if (wantedFlight == null)
            {
                Console.WriteLine("Let s tim ID-om ne postoji.");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }

            else
            {
                Console.WriteLine("Traženi let: ");
                Console.WriteLine($"{wantedFlight.ID} - {wantedFlight.StartLocation} -> {wantedFlight.EndLocation} - {wantedFlight.Departure:yyyy-MM-dd HH:mm} - {wantedFlight.Arrival:yyyy-MM-dd HH:mm} - {wantedFlight.Distance}km - {wantedFlight.Duration}");
            }

            Console.Write($"Jeste li sigurni da želite urediti let '{wantedFlight.Name}'?(da/ne):");
            var choice = Console.ReadLine();
            if (choice.ToLower() != "da")
            {
                Console.WriteLine("Uređivanje otkazano.");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }
            while (true)
            {
                Console.Write("Uneite novo vrijeme polaska (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime departure))
                {
                    wantedFlight.Departure = departure;
                    break;
                }
                else Console.WriteLine("Pogrešan format datuma/vremena!");
            }
            while (true)
            {
                Console.Write("Uneite vrijeme dolaska (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime arrival))
                {
                    if (arrival > wantedFlight.Departure)
                    {
                        wantedFlight.Arrival = arrival;
                        break;
                    }
                    else Console.WriteLine("Vrijeme dolaska mora biti nakon vremena polaska!");
                }
                else Console.WriteLine("Pogrešan format datuma/vremena!");
            }

            var availableCrews = Crews.Where(c => c.IsAvailable).ToList();
            if (!availableCrews.Any())
            {
                Console.WriteLine("Nema dostupnih posada za ovaj let!");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nDostupne posade:");
            for (int i = 0; i < availableCrews.Count; i++)
            {
                var crew = availableCrews[i];
                Console.WriteLine($"{i + 1}. {crew.Name} - Pilot: {crew.Pilot.Name} {crew.Pilot.Surname}, Kopilot: {crew.Copilot.Name} {crew.Copilot.Surname}");
            }

            int crewChoice;
            while (true)
            {
                Console.Write("Odaberite novu posadu: ");
                if (int.TryParse(Console.ReadLine(), out crewChoice) && crewChoice >= 1 && crewChoice <= availableCrews.Count)
                    break;

                Console.WriteLine("Neispravan odabir!");
            }
            if (wantedFlight.Crew != null)
                wantedFlight.Crew.IsAvailable = true;

            wantedFlight.Crew = availableCrews[crewChoice - 1];
            wantedFlight.Crew.IsAvailable = false;
            wantedFlight.UpdateTimestamp();
            Console.WriteLine("\nLet uspješno uređen!");
            Console.ReadKey();
        }
        public static void DeleteFlight()
        {
            Console.Write("\nUnesi ID leta koji želiš izbrisati: ");
            if (!int.TryParse(Console.ReadLine(), out int flightID))
            {
                Console.WriteLine("Neispravan unos ID-ja!");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }
            var wantedFlight = Flights.FirstOrDefault(flight => flight.ID == flightID);

            if (wantedFlight == null)
            {
                Console.WriteLine("Let s tim ID-om ne postoji.");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("Let za brisanje: ");
                Console.WriteLine($"{wantedFlight.ID} - {wantedFlight.StartLocation} -> {wantedFlight.EndLocation} - {wantedFlight.Departure:yyyy-MM-dd HH:mm} - {wantedFlight.Arrival:yyyy-MM-dd HH:mm} - {wantedFlight.Distance}km - {wantedFlight.Duration}");
            }
            Console.Write($"Jeste li sigurni da želite izbrisati let '{wantedFlight.Name}'?(da/ne):");
            var choice = Console.ReadLine();
            if (choice.ToLower() != "da")
            {
                Console.WriteLine("Uređivanje otkazano.");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }

            if (wantedFlight.Crew != null)
                wantedFlight.Crew.IsAvailable = true;
            foreach (var passenger in Passengers)
            {
                var reservationsToRemove = passenger.Reservations.Where(r => r.Flight == wantedFlight).ToList();
                foreach (var reservation in reservationsToRemove)
                    passenger.Reservations.Remove(reservation);
            }
            wantedFlight.Airplane.FlightsNumber--;
            Flights.Remove(wantedFlight);
            Console.WriteLine("\nLet je uspješno izbrisan!");
            Console.ReadKey();

        }
        public static void PrintAllAirplanes()
        {
            Console.Clear();
            Console.WriteLine("Popis svih aviona: ");
            foreach(var airplane in Airplanes) 
                Console.WriteLine($"{airplane.ID} - {airplane.Name} - {airplane.Year} - {airplane.FlightsNumber}");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }

    }
}