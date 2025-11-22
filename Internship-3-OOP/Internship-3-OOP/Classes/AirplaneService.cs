
namespace Internship_3_OOP.Classes
{
    public class AirplaneService
    {
        public static void PrintAllAirplanes()
        {
            Console.Clear();
            Console.WriteLine("Popis svih aviona: ");
            foreach (var airplane in InitialData.Airplanes)
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
            InitialData.Airplanes.Add(airplane);
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
                var wantedAirplane = InitialData.Airplanes.FirstOrDefault(airplane => airplane.ID == airplaneID);

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
                var wantedAirplane = InitialData.Airplanes.FirstOrDefault(airplane => airplane.Name.Equals(airplaneName, StringComparison.OrdinalIgnoreCase));
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
            foreach (var airplane in InitialData.Airplanes)
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
                var wantedAirplane = InitialData.Airplanes.FirstOrDefault(airplane => airplane.ID == airplaneID);

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

                foreach (var flight in InitialData.Flights)
                {
                    if (flight.Airplane != null && flight.Airplane.ID == wantedAirplane.ID)
                    {
                        flight.Airplane = null;
                    }
                }
                InitialData.Airplanes.Remove(wantedAirplane);
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
                var wantedAirplane = InitialData.Airplanes.FirstOrDefault(airplane => airplane.Name.Equals(airplaneName, StringComparison.OrdinalIgnoreCase));
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

                foreach (var flight in InitialData.Flights)
                {
                    if (flight.Airplane != null && flight.Airplane.ID == wantedAirplane.ID)
                    {
                        flight.Airplane = null;
                    }
                }
                InitialData.Airplanes.Remove(wantedAirplane);
                Console.WriteLine("\nAvion je uspješno izbrisan!");
                Console.Write("Pritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
                return;
            }
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
    }
}
