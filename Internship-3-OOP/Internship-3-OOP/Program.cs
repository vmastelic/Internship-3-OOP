using Internship_3_OOP.Classes;
namespace Internship_3_OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InitialData.Initialize();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===APLIKACIJA ZA UPRAVLJANJE AERODROMOM===");
                Console.WriteLine("1 - Putnici");
                Console.WriteLine("2 - Letovi");
                Console.WriteLine("3 - Avioni");
                Console.WriteLine("4 - Posada");
                Console.WriteLine("5 - Izlaz iz programa");
                Console.Write("\nOdabir: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": PassengersMenu(); break;
                    case "2": FlightsMenu(); break;
                    case "3":AirplanesMenu(); break;
                    case "5": return;
                }
            }
        }
        static void PassengersMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===PUTNICI===");
                Console.WriteLine("1 - Registracija");
                Console.WriteLine("2 - Prijava");
                Console.WriteLine("3 - Povratak na prethodni izbornik");
                Console.Write("\nOdabir: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": PassengerRegistration(); break;
                    case "2": PassengerLogin(); break;
                    case "3": return;
                }
            }
        }
        static void FlightsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===LETOVI===");
                Console.WriteLine("1 - Prikaz svih letova");
                Console.WriteLine("2 - Dodavanje leta");
                Console.WriteLine("3 - Pretraživanje letova");
                Console.WriteLine("4 - Uređivanje leta");
                Console.WriteLine("5 - Brisanje leta");
                Console.WriteLine("6 - Povratak na prethodni izbornik");
                Console.Write("\nOdabir: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": InitialData.PrintAllFlights(); break;
                    case "2": AddFlight(); break;
                    case "3": InitialData.FindFlight(); break;
                    case "4": InitialData.EditFlight(); break;   
                    case "5": InitialData.DeleteFlight(); break;
                    case "6": return;
                }
            }
        }
        static void AirplanesMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===AVIONI===");
                Console.WriteLine("1 - Prikaz svih aviona");
                Console.WriteLine("2 - Dodavanje aviona");
                Console.WriteLine("3 - Pretraživanje aviona");
                Console.WriteLine("4 - Brisanje aviona");
                Console.WriteLine("5 - Povratak na prethodni izbornik");
                Console.Write("\nOdabir: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": InitialData.PrintAllAirplanes(); break;
                    case "2": InitialData.AddAirplane(); break;
                    case "3": InitialData.FindAirplane();  break;
                    case "4": InitialData.DeleteAirplane(); break;
                    case "5": return;
                }
            }
        }
        static void AddFlight()
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
        static void PassengerRegistration()
        {
            Passenger passenger = new Passenger();
            Console.Clear();
            Console.WriteLine("===REGISTRACIJA PUTNIKA===");

            while (true)
            {
                Console.Write("Unesite ime: ");
                var name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name)) { passenger.Name = name; break; }
                else Console.WriteLine("Ime ne smije biti prazno, pokusajte ponovno.");
            }
            while (true)
            {
                Console.Write("Unesi prezime: ");
                var surname = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(surname)) { passenger.Surname = surname; break; }
                else Console.WriteLine("Prezime ne smije biti prazno, pokusajte ponovno.");
            }

            DateOnly birthDate;
            while (true)
            {
                Console.Write("Unesi datum rođenja (yyyy-mm-dd): ");
                if (DateOnly.TryParse(Console.ReadLine(), out birthDate) && birthDate.Year > 1925 && birthDate.Year < 2025)
                {
                    passenger.BirthDate = birthDate;
                    break;
                }
                Console.WriteLine("Pogrešan format datuma(datum mora biti nakon 1925. i prije 2025. godine)!");
            }
            while (true)
            {
                Console.Write("Unesi email: ");
                var email = Console.ReadLine();

                if (InitialData.Passengers.Any(passenger => passenger.Email == email))
                {
                    Console.WriteLine("Korisnik s tim emailom već postoji!");
                    Console.WriteLine("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                else if (email != null)
                {
                    passenger.Email = email;
                    break;
                }
            }
            while (true)
            {
                Console.Write("Unesite lozinku: ");
                var passwordFirst = Console.ReadLine();
                Console.Write("Ponovite lozinku: ");
                var passwordSecond = Console.ReadLine();

                if (passwordFirst != passwordSecond)
                {
                    Console.WriteLine("Lozinke se ne podudaraju!");
                    Console.WriteLine("Pokušajte ponovno.");
                }
                else if (passwordFirst != null)
                { passenger.Password = passwordFirst; break; }
            }


            InitialData.Passengers.Add(passenger);
            Console.WriteLine($"Putnik {passenger.Name} {passenger.Surname} registriran.");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadLine();
        }
        static void PassengerLogin()
        {
            Console.Clear();
            Console.WriteLine("===PRIJAVA KORISNIKA===");
            Console.Write("Unesite email: ");
            var email = Console.ReadLine();
            if (email == null) return;

            var passenger = InitialData.Passengers.FirstOrDefault(passenger => passenger.Email == email);
            if (passenger == null)
            {
                Console.WriteLine("Ne postoji taj email.");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadLine();
                return;
            }
            Console.Write("Unesite loznku: ");
            string password = Console.ReadLine();
            if (password == passenger.Password)
            {
                Console.WriteLine("Uspješna prijava");
                LoggedInMenu(passenger);
                return;
            }
            Console.WriteLine("Nevažeća lozinka!");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadLine();
        }
        static void LoggedInMenu(Passenger passenger)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"==={passenger.Name} {passenger.Surname}===");
                Console.WriteLine("1 - Prikaz svih letova");
                Console.WriteLine("2 - Odabir leta");
                Console.WriteLine("3 - Pretraživanje letova");
                Console.WriteLine("4 - Otkazivanje leta");
                Console.WriteLine("5 - Povratak na prethodni izbornik");
                Console.Write("\nOdabir: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": passenger.PrintReservedFlights(passenger); break;
                    case "2": passenger.ReserveFlight(passenger); break;
                    case "3": passenger.FindFlight(passenger); break;
                    case "4": passenger.CancelFlight(passenger); break;
                    case "5": return;
                }
            }
        }
    }
}