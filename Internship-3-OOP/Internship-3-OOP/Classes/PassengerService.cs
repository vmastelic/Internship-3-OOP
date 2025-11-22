
namespace Internship_3_OOP.Classes
{
    public class PassengerService
    {
        public static void PassengerRegistration()
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
        public static void PassengerLogin()
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
        public static void LoggedInMenu(Passenger passenger)
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
                    case "1": PrintReservedFlights(passenger); break;
                    case "2": ReserveFlight(passenger); break;
                    case "3": FindFlight(passenger); break;
                    case "4": CancelFlight(passenger); break;
                    case "5": return;
                }
            }
        }
        public static void AddReservation(Reservation reservation, Passenger passenger)
        {
            if (!passenger.Reservations.Contains(reservation))
                passenger.Reservations.Add(reservation);
        }
        public static void PrintReservedFlights(Passenger passenger)
        {
            Console.Clear();
            foreach (var reservation in passenger.Reservations)
                Console.WriteLine($"{reservation.Flight.ID} - {reservation.Flight.Name} - {reservation.Flight.Departure.ToString("yyyy-MM-dd")} - {reservation.Flight.Arrival.ToString("yyyy-MM-dd")} - {reservation.Flight.Distance} - {reservation.Flight.Duration}");

            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
        public static void ReserveFlight(Passenger passenger)
        {
            Console.Clear();

            var availableFlights = InitialData.Flights.Where(flight => !passenger.Reservations.Any(r => r.Flight == flight)).ToList();
            if (availableFlights.Count == 0)
            {
                Console.WriteLine("Nema dostupnih letova za rezervaciju.");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Dostupni letovi: ");
            foreach (var flight in availableFlights)
                Console.WriteLine($"{flight.ID} - {flight.Name} - {flight.Departure:yyyy-MM-dd} - {flight.Arrival:yyyy-MM-dd} - {flight.Distance}km - {flight.Duration}");

            Console.Write("\nUnesite ID leta koji zelite rezervirati: ");
            if (!int.TryParse(Console.ReadLine(), out int flightID))
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

            TicketType ticketType;
            while (true)
            {
                Console.Write("Odabir vrstu karte: ");
                Console.WriteLine("\n0 - Standard\n1 - Buissnes\n2 - Vip");
                Console.Write("Odabir: ");
                if (int.TryParse(Console.ReadLine(), out int ticketChoice) && ticketChoice >= 0 && ticketChoice <= 2)
                {
                    ticketType = (TicketType)ticketChoice;
                    break;
                }
                else Console.WriteLine("Neispravan unos! Unesite broj od 0 do 2.");
            }

            if (selectedFlight.GetAvailableSeats(ticketType) == 0)
            {
                Console.WriteLine("Karte su rasprodane za tu kategoriju.");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;

            }

            var selectedReservation = new Reservation(passenger, selectedFlight, ticketType);
            passenger.Reservations.Add(selectedReservation);
            selectedFlight.Passengers.Add(passenger);
            Console.WriteLine($"\nLet '{selectedFlight.Name}' uspješno rezerviran sa kartom {ticketType}!");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
        public static void FindFlight(Passenger passenger)
        {
            var reservedFlights = InitialData.Flights.Where(flight => passenger.Reservations.Any(r => r.Flight == flight)).ToList();
            Console.Clear();
            Console.WriteLine("Pretrazi let po\na) ID\nb) Ime");
            Console.Write("\nOdabir: ");
            var choice = Console.ReadLine();
            if (choice == "a")
            {
                Console.Write("\nUnesi ID:");
                if (!int.TryParse(Console.ReadLine(), out int flightID))
                {
                    Console.WriteLine("Neispravan ID!");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                var wantedFlight = reservedFlights.FirstOrDefault(flight => flight.ID == flightID);
                if (wantedFlight == null)
                {
                    Console.WriteLine("Let s tim ID-om nije rezerviran.");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Trazeni let: ");
                Console.WriteLine($"{wantedFlight.ID} - {wantedFlight.Name} - {wantedFlight.Departure:yyyy-MM-dd} - {wantedFlight.Arrival:yyyy-MM-dd} - {wantedFlight.Distance}km - {wantedFlight.Duration}");
            }
            else if (choice == "b")
            {
                Console.Write("Unesi ime: ");
                var flightName = Console.ReadLine();
                if (flightName == null)
                {
                    Console.WriteLine("Neispravan unos imena");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                var wantedFlight = reservedFlights.FirstOrDefault(flight => flight.Name == flightName);
                if (wantedFlight == null)
                {
                    Console.WriteLine("Let s tim ID-om nije rezerviran.");
                    Console.Write("Pritisnite bilo koju tipku za nastavak...");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Trazeni let: ");
                Console.WriteLine($"{wantedFlight.ID} - {wantedFlight.Name} - {wantedFlight.Departure:yyyy-MM-dd} - {wantedFlight.Arrival:yyyy-MM-dd} - {wantedFlight.Distance}km - {wantedFlight.Duration}");
            }
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
        public static void CancelFlight(Passenger passenger)
        {
            Console.Clear();
            Console.WriteLine("Rezervirani letovi: ");
            var reservedFlights = InitialData.Flights.Where(flight => passenger.Reservations.Any(r => r.Flight == flight)).ToList();
            foreach (var flight in reservedFlights)
                Console.WriteLine($"{flight.ID} - {flight.Name} - {flight.Departure:yyyy-MM-dd} - {flight.Arrival:yyyy-MM-dd} - {flight.Distance}km - {flight.Duration}");

            Console.Write("Unesi ID leta koji zeliš otkazati: ");
            if (!int.TryParse(Console.ReadLine(), out int flightID))
            {
                Console.WriteLine("Neispravan ID!");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }
            var wantedFlight = reservedFlights.FirstOrDefault(flight => flight.ID == flightID);
            if (wantedFlight == null)
            {
                Console.WriteLine("Let s tim ID-om nije rezerviran.");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }
            if (wantedFlight.Departure <= DateTime.Now.AddHours(24) && wantedFlight.Departure > DateTime.Now)
            {
                Console.WriteLine("Ne možete otkazati let koji polieće za manje od 24h.");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }

            var reservationToRemove = passenger.Reservations.First(reservation => reservation.Flight.ID == flightID);
            if (reservationToRemove == null)
            {
                Console.WriteLine("Rezervacija nije pronađena.");
                Console.ReadKey();
                return;
            }
            Console.Write($"Jeste li sigurni da želite otkazati let '{wantedFlight.Name}'?(da/ne):");
            var choice = Console.ReadLine();
            if (choice.ToLower() != "da")
            {
                Console.WriteLine("Otkazivanje otkazano.");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }
            passenger.Reservations.Remove(reservationToRemove);
            wantedFlight.Passengers.Remove(passenger);
            Console.WriteLine("Otkazivanje uspjesno.");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
            return;

        }
    }

}
