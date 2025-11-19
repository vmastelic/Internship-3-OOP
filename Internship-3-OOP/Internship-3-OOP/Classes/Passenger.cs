namespace Internship_3_OOP.Classes
{
    public class Passenger : BaseClass
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public Passenger() { }
        public Passenger(string name, string surname, DateOnly birthDate, string email, string password)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            Email = email;
            Password = password;
        }
        public void AddReservation(Reservation reservation)
        {
            if (!Reservations.Contains(reservation))
                Reservations.Add(reservation);
        }
        public void PrintReservedFlights(Passenger passenger)
        {
            Console.Clear();
            foreach (var reservation in Reservations)
                Console.WriteLine($"{reservation.Flight.ID} - {reservation.Flight.Name} - {reservation.Flight.Departure.ToString("yyyy-MM-dd")} - {reservation.Flight.Arrival.ToString("yyyy-MM-dd")} - {reservation.Flight.Distance} - {reservation.Flight.Duration}");

            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
        public void ReserveFlight(Passenger passenger)
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

            if(selectedFlight.GetAvailableSeats(ticketType) == 0)
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
        public void FindFlight(Passenger passenger)
        {
            var reservedFlights = InitialData.Flights.Where(flight => passenger.Reservations.Any(r => r.Flight == flight)).ToList();
            Console.Clear();
            Console.WriteLine("Pretrazi let po\na) ID\nb) Ime");
            Console.Write("\nOdabir: ");
            var choice = Console.ReadLine();
            if(choice == "a")
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
        public void CancelFlight(Passenger passenger)
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