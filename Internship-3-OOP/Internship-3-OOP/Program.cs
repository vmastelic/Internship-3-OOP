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
                    case "3": AirplanesMenu(); break;
                    case "4": CrewsMenu(); break;
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
                    case "1": PassengerService.PassengerRegistration(); break;
                    case "2": PassengerService.PassengerLogin(); break;
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
                    case "1": FlightService.PrintAllFlights(); break;
                    case "2": FlightService.AddFlight(); break;
                    case "3": FlightService.FindFlight(); break;
                    case "4": FlightService.EditFlight(); break;   
                    case "5": FlightService.DeleteFlight(); break;
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
                    case "1": AirplaneService.PrintAllAirplanes(); break;
                    case "2": AirplaneService.AddAirplane(); break;
                    case "3": AirplaneService.FindAirplane();  break;
                    case "4": AirplaneService.DeleteAirplane(); break;
                    case "5": return;
                }
            }
        }
        static void CrewsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===POSADE===");
                Console.WriteLine("1 - Prikaz svih posada");
                Console.WriteLine("2 - Kreiranje nove posade");
                Console.WriteLine("3 - Dodavanje osobe");
                Console.WriteLine("4 - Povratak na prethodni izbornik");
                Console.Write("\nOdabir: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": CrewService.PrintAllCrews(); break;
                    case "2": CrewService.AddCrew(); break;
                    case "3": CrewService.AddFreeCrewMember(); break;
                    case "4": return;
                }
            }
        }
    }
}