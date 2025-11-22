using System.Net.Sockets;

namespace Internship_3_OOP.Classes
{
    internal class InitialData
    {
        public static List<Passenger> Passengers = new List<Passenger>();
        public static List<Flight> Flights = new List<Flight>();
        public static List<Crew> Crews = new List<Crew>();
        public static List<Airplane> Airplanes = new List<Airplane>();
        public static List<CrewMember> FreeCrewMembers = new List<CrewMember>();

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

            var freePilot = new CrewMember("Mate", "Putnik", Gender.Male, Position.Pilot, new DateOnly(1975, 3, 2));
            var freeCopilot = new CrewMember("Luka", "Botica", Gender.Male, Position.Copilot, new DateOnly(1975, 3, 2));
            var freeStewardess1 = new CrewMember("Marta", "Vukić", Gender.Female, Position.Stewardess, new DateOnly(1975, 3, 2));
            var freeStewardess2 = new CrewMember("Lana", "Vukas", Gender.Female, Position.Stewardess, new DateOnly(1975, 3, 2));

            FreeCrewMembers.Add(freePilot);
            FreeCrewMembers.Add(freeCopilot);
            FreeCrewMembers.Add(freeStewardess1);
            FreeCrewMembers.Add(freeStewardess2);

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
            Console.WriteLine("Popis svih letova: ");
            foreach (var flight in Flights)
                Console.WriteLine($"{flight.ID} - {flight.Name} - {flight.Departure:yyyy-MM-dd} - {flight.Arrival:yyyy-MM-dd} - {flight.Distance}km - {flight.Duration}");
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
                Console.WriteLine("Brisanje otkazano.");
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
            foreach (var airplane in Airplanes)
                Console.WriteLine($"{airplane.ID} - {airplane.Name} - {airplane.Year} - {airplane.FlightsNumber}");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
        public static void AddAirplane()
        {
            Console.Clear();
            Airplane airplane = new Airplane();
            Console.WriteLine("===DODAVANJE AVIONA===");
            while (true)
            {
                Console.Write("Unesite naziv aviona: ");
                var name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name)) { airplane.Name = name; break; }
                else Console.WriteLine("Naziv ne smije biti prazan, pokušajte ponovno.");
            }
            while (true)
            {
                Console.Write("Unesite godinu porizvodnje aviona: ");
                if (int.TryParse(Console.ReadLine(), out int year) && year <= 2025 && year >= 1925)
                {
                    airplane.Year = year;
                    break;
                }
                else Console.WriteLine("Neispravan unos godine proizvodnje aviona");
            }

            while (true)
            {
                Console.WriteLine("\nOdabir vrste karata:");
                Console.WriteLine("0 - Standard");
                Console.WriteLine("1 - Business");
                Console.WriteLine("2 - VIP");
                Console.WriteLine("3 - Kraj unosa");
                Console.Write("Odabir: ");

                if (!int.TryParse(Console.ReadLine(), out int ticketChoice) || ticketChoice < 0 || ticketChoice > 3)
                {
                    Console.WriteLine("Neispravan unos! Unesite broj od 0 do 3.");
                    continue;
                }

                if (ticketChoice == 3) break;

                TicketType ticketType = (TicketType)ticketChoice;
                Console.Clear();
                Console.Write($"Unesite broj sjedećih mjesta za kategoriju {ticketType}: ");
                if (!int.TryParse(Console.ReadLine(), out int seatsNumber) || seatsNumber < 0 || seatsNumber > 300)
                {
                    Console.WriteLine("Neispravan broj sjedišta! Unesite broj između 0 i 300.");
                    continue;
                }

                switch (ticketType)
                {
                    case TicketType.Standard:
                        airplane.StandardCapacity = seatsNumber;
                        break;
                    case TicketType.Business:
                        airplane.BusinessCapacity = seatsNumber;
                        break;
                    case TicketType.VIP:
                        airplane.VipCapacity = seatsNumber;
                        break;
                }
            }
            Airplanes.Add(airplane);
            Console.WriteLine($"Avion {airplane.Name} uspješno dodan.");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
            return;

        }
        public static void FindAirplane()
        {
            Console.Clear();
            Console.WriteLine("Pretrazi avion po:\na) ID\nb) Ime");
            Console.Write("\nOdabir: ");
            var choice = Console.ReadLine();

            if (choice == "a")
            {
                Console.Write("\nUnesi ID: ");
                if (!int.TryParse(Console.ReadLine(), out int airplaneID))
                {
                    Console.WriteLine("Neispravan ID!");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                var wantedAirplane = Airplanes.FirstOrDefault(airplane => airplane.ID == airplaneID);

                if (wantedAirplane == null)
                {
                    Console.WriteLine("Avion s tim imenom ne postoji.");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("Traženi avion: ");
                    Console.WriteLine($"{wantedAirplane.ID} - {wantedAirplane.Name} - {wantedAirplane.Year} - {wantedAirplane.FlightsNumber}");
                }
            }
            else if (choice == "b")
            {
                Console.Write("\nUnesi ime aviona: ");
                var airplaneName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(airplaneName))
                {
                    Console.WriteLine("Neispravan unos imena");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                var wantedAirplane = Airplanes.FirstOrDefault(airplane => airplane.Name.Equals(airplaneName, StringComparison.OrdinalIgnoreCase));
                if (wantedAirplane == null)
                    Console.WriteLine("Avion s tim imenom ne postoji.");
                else
                {
                    Console.WriteLine("Traženi avion: ");
                    Console.WriteLine($"{wantedAirplane.ID} - {wantedAirplane.Name} - {wantedAirplane.Year} - {wantedAirplane.FlightsNumber}");
                }
            }
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
        public static void DeleteAirplane()
        {
            Console.Clear();
            Console.WriteLine("Popis svih aviona: ");
            foreach (var airplane in Airplanes)
                Console.WriteLine($"{airplane.ID} - {airplane.Name}");
            Console.WriteLine("Izbrisite avion po:\na) ID\nb) Ime");
            Console.Write("\nOdabir: ");
            var choice = Console.ReadLine();

            if (choice == "a")
            {
                Console.Write("\nUnesi ID aviona koji želiš izbrisati: ");
                if (!int.TryParse(Console.ReadLine(), out int airplaneID))
                {
                    Console.WriteLine("Neispravan unos ID-ja!");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                var wantedAirplane = Airplanes.FirstOrDefault(airplane => airplane.ID == airplaneID);

                if (wantedAirplane == null)
                {
                    Console.WriteLine("Avion s tim ID-om ne postoji.");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("Avion za brisanje: ");
                    Console.WriteLine($"{wantedAirplane.ID} - {wantedAirplane.Name} - {wantedAirplane.Year}");
                }
                Console.Write($"Jeste li sigurni da želite izbrisati avion '{wantedAirplane.Name}'?(da/ne):");
                var confirm = Console.ReadLine();
                if (confirm.ToLower() != "da")
                {
                    Console.WriteLine("Brisanje otkazano.");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }

                foreach (var flight in Flights)
                {
                    if (flight.Airplane != null && flight.Airplane.ID == wantedAirplane.ID)
                    {
                        flight.Airplane = null;
                    }
                }
                Airplanes.Remove(wantedAirplane);
                Console.WriteLine("\nAvion je uspješno izbrisan!");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }
            else if (choice == "b")
            {
                Console.Write("\nUnesi ime aviona: ");
                var airplaneName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(airplaneName))
                {
                    Console.WriteLine("Neispravan unos imena");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                var wantedAirplane = Airplanes.FirstOrDefault(airplane => airplane.Name.Equals(airplaneName, StringComparison.OrdinalIgnoreCase));
                if (wantedAirplane == null)
                {
                    Console.WriteLine("Avion s tim imenom ne postoji.");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("Avion za brisanje: ");
                    Console.WriteLine($"{wantedAirplane.ID} - {wantedAirplane.Name} - {wantedAirplane.Year}");
                }
                Console.Write($"Jeste li sigurni da želite izbrisati avion '{wantedAirplane.Name}'?(da/ne):");
                var confirm = Console.ReadLine();
                if (confirm.ToLower() != "da")
                {
                    Console.WriteLine("Brisanje otkazano.");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }

                foreach (var flight in Flights)
                {
                    if (flight.Airplane != null && flight.Airplane.ID == wantedAirplane.ID)
                    {
                        flight.Airplane = null;
                    }
                }
                Airplanes.Remove(wantedAirplane);
                Console.WriteLine("\nAvion je uspješno izbrisan!");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }

        public static void PrintAllCrews()
        {
            Console.Clear();
            Console.WriteLine("Popis svih posada: ");
            foreach (var crew in Crews)
            {
                Console.WriteLine($"\nNaziv posade: {crew.Name}, članovi:");
                Console.WriteLine($"{crew.Pilot.Name} {crew.Pilot.Surname} - Pozicija: {crew.Pilot.Position} - Godište: {crew.Pilot.BirthDate:yyyy}");
                Console.WriteLine($"{crew.Copilot.Name} {crew.Copilot.Surname} - Pozicija: {crew.Copilot.Position} - Godište: {crew.Copilot.BirthDate:yyyy}");
                Console.WriteLine($"{crew.Stewardess1.Name} {crew.Stewardess1.Surname} - Pozicija: {crew.Stewardess1.Position} - Godište: {crew.Stewardess1.BirthDate:yyyy}");
                Console.WriteLine($"{crew.Stewardess2.Name} {crew.Stewardess2.Surname} - Pozicija: {crew.Stewardess2.Position} - Godište: {crew.Stewardess2.BirthDate:yyyy}");
            }
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
        public static void AddCrew()
        {
            Console.Clear();
            Console.WriteLine("=== KREIRANJE NOVE POSADE ===");

            Console.Write("\nUnesite ime posade: ");
            string crewName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(crewName))
            {
                Console.WriteLine("Neispravan unos imena posade!");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }

            var pilot = SelectCrewMemberByPosition(Position.Pilot);
            if (pilot == null) return;

            var copilot = SelectCrewMemberByPosition(Position.Copilot);
            if (copilot == null) return;

            var stewardess1 = SelectCrewMemberByPosition(Position.Stewardess);
            if (stewardess1 == null) return;

            var stewardess2 = SelectCrewMemberByPosition(Position.Stewardess);
            if (stewardess2 == null) return;

            var newCrew = new Crew
            {
                Name = crewName,
                Pilot = pilot,
                Copilot = copilot,
                Stewardess1 = stewardess1,
                Stewardess2 = stewardess2
            };

            Crews.Add(newCrew);
            FreeCrewMembers.Remove(pilot);
            FreeCrewMembers.Remove(copilot);
            FreeCrewMembers.Remove(stewardess1);
            FreeCrewMembers.Remove(stewardess2);

            Console.WriteLine("\nPosada uspješno kreirana!");
            Console.WriteLine($"Naziv: {newCrew.Name}");
            Console.WriteLine($"Pilot: {pilot.Name} {pilot.Surname}");
            Console.WriteLine($"Kopilot: {copilot.Name} {copilot.Surname}");
            Console.WriteLine($"Stjuardese: {stewardess1.Name} {stewardess1.Surname} i {stewardess2.Name} {stewardess2.Surname}");

            Console.Write("\nPritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
        private static CrewMember SelectCrewMemberByPosition(Position position)
        {
            var available = FreeCrewMembers.Where(m => m.Position == position).ToList();
            if (available.Count == 0)
            {
                Console.WriteLine($"\nNema dostupnih članova za poziciju {position}!");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return null;
            }
            Console.WriteLine($"\nOdaberi {position}:");
            foreach (var member in available)
            {
                Console.WriteLine($"{member.ID} - {member.Name} {member.Surname}");
            }

            Console.Write($"Odaberite ID za {position}: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Neispravan ID!");
                Console.ReadKey();
                return null;
            }

            var chosen = available.FirstOrDefault(member => member.ID == id);
            if (chosen == null)
            {
                Console.WriteLine("Član posade s tim ID-om ne postoji u dostupnima.");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return null;
            }

            return chosen;
        }
        public static void AddFreeCrewMember()
        {
            Console.Clear();
            CrewMember crewMember = new CrewMember();
            Console.WriteLine("===DODAVANJE ČLANA POSADE===");

            while (true)
            {
                Console.Write("Unesite ime člana posade: ");
                var name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    crewMember.Name = name;
                    break;
                }
                Console.WriteLine("Ime ne smije biti prazno, pokušajte ponovno.");
            }

            while (true)
            {
                Console.Write("Unesite prezime člana posade: ");
                var surname = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(surname))
                {
                    crewMember.Surname = surname;
                    break;
                }
                Console.WriteLine("Prezime ne smije biti prazno, pokušajte ponovno.");
            }
            while (true)
            {
                Console.Write("Unesite datum rođenja (yyyy-MM-dd): ");
                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly birthDate)
                    && birthDate.Year >= 1925 && birthDate.Year <= 2007)
                {
                    crewMember.BirthDate = birthDate;
                    break;
                }
                Console.WriteLine("Pogrešan format datuma/vremena ili nedozvoljena godina!");
            }
            while (true)
            {
                Console.WriteLine("\nSpol:");
                Console.WriteLine("0 - Muško");
                Console.WriteLine("1 - Žensko");
                Console.Write("Odabir: ");

                if (int.TryParse(Console.ReadLine(), out int genderChoice) && genderChoice >= 0 && genderChoice <= 1)
                {
                    crewMember.Gender = (Gender)genderChoice;
                    break;
                }
                Console.WriteLine("Neispravan unos! Unesite 0 ili 1.");
            }
            while (true)
            {
                Console.WriteLine("\nPozicija:");
                Console.WriteLine("0 - Pilot");
                Console.WriteLine("1 - Copilot");
                Console.WriteLine("2 - Stewardess");
                Console.Write("Odabir: ");

                if (int.TryParse(Console.ReadLine(), out int positionChoice) && positionChoice >= 0 && positionChoice <= 2)
                {
                    crewMember.Position = (Position)positionChoice;
                    break;
                }
                Console.WriteLine("Neispravan unos! Odaberite broj 0-2.");
            }
            FreeCrewMembers.Add(crewMember);
            Console.WriteLine($"\nČlan posade '{crewMember.Name} {crewMember.Surname}' uspješno dodan kao {crewMember.Position}.");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
    }
}