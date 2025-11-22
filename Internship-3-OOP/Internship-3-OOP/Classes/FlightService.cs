
namespace Internship_3_OOP.Classes
{
    internal class FlightService
    {
        public static void PrintAllFlights()
        {
            Console.Clear();
            Console.WriteLine("Popis svih letova: ");
            foreach (var flight in InitialData.Flights)
                Console.WriteLine($"{flight.ID} - {flight.Name} - {flight.Departure:yyyy-MM-dd} - {flight.Arrival:yyyy-MM-dd} - {flight.Distance}km - {flight.Duration}");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
        public static void AddFlight()
        {
            Flight flight = new Flight();
            Console.Clear();
            Console.WriteLine("===DODAVANJE LETA===");
            while (true)
            {
                Console.Write("Unesite naziv leta: ");
                var name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name)) { flight.Name = name; break; }
                else Console.WriteLine("Ime ne smije biti prazno, pokušajte ponovno.");
            }
            while (true)
            {
                Console.Write("Unesite mjesto polaska: ");
                var startLocation = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(startLocation)) { flight.StartLocation = startLocation; break; }
                else { Console.WriteLine("Mjesto polaska ne smije biti prazno!"); }
            }
            while (true)
            {
                Console.Write("Unesite mjesto dolaska: ");
                var endLocation = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(endLocation)) { flight.EndLocation = endLocation; break; }
                else Console.WriteLine("Mjesto dolaska ne smije biti prazno!");
            }
            while (true)
            {
                Console.Write("Uneite vrijeme polaska (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime departure))
                {
                    flight.Departure = departure;
                    break;
                }
                else Console.WriteLine("Pogrešan format datuma/vremena!");
            }
            while (true)
            {
                Console.Write("Uneite vrijeme dolaska (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime arrival))
                {
                    if (arrival > flight.Departure)
                    {
                        flight.Arrival = arrival;
                        break;
                    }
                    else Console.WriteLine("Vrijeme dolaska mora biti nakon vremena polaska!");
                }
                else Console.WriteLine("Pogrešan format datuma/vremena!");
            }

            flight.Duration = flight.Arrival - flight.Departure;

            while (true)
            {
                Console.Write("Unesite udaljenost leta u km: ");
                if (int.TryParse(Console.ReadLine(), out int distance) && distance > 0)
                {
                    flight.Distance = distance;
                    break;
                }
                else Console.WriteLine("Neispravan unos kilometraže!");
            }

            var availableCrews = InitialData.Crews.Where(c => c.IsAvailable).ToList();
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
                Console.Write("Odaberite posadu (unesite broj): ");
                if (int.TryParse(Console.ReadLine(), out crewChoice) && crewChoice >= 1 && crewChoice <= availableCrews.Count)
                {
                    flight.Crew = availableCrews[crewChoice - 1];
                    flight.Crew.IsAvailable = false;
                    break;
                }
                else Console.WriteLine("Neispravan odabir, pokušajte ponovno.");
            }

            var availablePlanes = InitialData.Airplanes.ToList();

            Console.WriteLine("\nDostupni avioni:");
            for (int i = 0; i < availablePlanes.Count; i++)
            {
                var plane = availablePlanes[i];
                Console.WriteLine($"{i + 1}. {plane.Name}, Godina proizvodnje: {plane.Year}");
            }

            int planeChoice;
            while (true)
            {
                Console.Write("Odaberite avion (unesite broj): ");
                if (int.TryParse(Console.ReadLine(), out planeChoice) && planeChoice >= 1 && planeChoice <= availablePlanes.Count)
                {
                    flight.Airplane = availablePlanes[planeChoice - 1];
                    availablePlanes[planeChoice - 1].FlightsNumber++;
                    break;
                }
                else Console.WriteLine("Neispravan odabir, pokušajte ponovno.");
            }


            InitialData.Flights.Add(flight);
            Console.WriteLine($"Let {flight.Name} uspješno dodan.");
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
                var wantedFlight = InitialData.Flights.FirstOrDefault(flight => flight.ID == flightID);

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
                    if (wantedFlight.Airplane != null)
                    {
                        Console.WriteLine($"Avion: {wantedFlight.Airplane.Name}");
                        Console.WriteLine($"Godina proizvodnje: {wantedFlight.Airplane.Year}");
                    }
                    else
                    {
                        Console.WriteLine("Avion nije dodijeljen ovom letu.");
                    }
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
                var wantedFlight = InitialData.Flights.FirstOrDefault(flight => flight.Name.Equals(flightName, StringComparison.OrdinalIgnoreCase));
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
                    if (wantedFlight.Airplane != null)
                    {
                        Console.WriteLine($"Avion: {wantedFlight.Airplane.Name}");
                        Console.WriteLine($"Godina proizvodnje: {wantedFlight.Airplane.Year}");
                    }
                    else
                    {
                        Console.WriteLine("Avion nije dodijeljen ovom letu.");
                    }
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
            var wantedFlight = InitialData.Flights.FirstOrDefault(flight => flight.ID == flightID);

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

            var availableCrews = InitialData.Crews.Where(c => c.IsAvailable).ToList();
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
            Console.WriteLine("Popis svih letova: ");
            foreach (var flight in InitialData.Flights)
                Console.WriteLine($"{flight.ID} - {flight.Name} - {flight.Departure:yyyy-MM-dd} - {flight.Arrival:yyyy-MM-dd} - {flight.Distance}km - {flight.Duration}");
            Console.Write("\nUnesi ID leta koji želiš izbrisati: ");
            if (!int.TryParse(Console.ReadLine(), out int flightID))
            {
                Console.WriteLine("Neispravan unos ID-ja!");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }
            var wantedFlight = InitialData.Flights.FirstOrDefault(flight => flight.ID == flightID);

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
                Console.WriteLine("Brisanje otkazano.");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }

            if (wantedFlight.Crew != null)
                wantedFlight.Crew.IsAvailable = true;
            foreach (var passenger in InitialData.Passengers)
            {
                var reservationsToRemove = passenger.Reservations.Where(r => r.Flight == wantedFlight).ToList();
                foreach (var reservation in reservationsToRemove)
                    passenger.Reservations.Remove(reservation);
            }
            wantedFlight.Airplane.FlightsNumber--;
            InitialData.Flights.Remove(wantedFlight);
            Console.WriteLine("\nLet je uspješno izbrisan!");
            Console.ReadKey();

        }
    }
}
