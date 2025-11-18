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
                switch(choice)
                {
                    case "1":
                        PassengersMenu();
                        break;
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
                    case "3": return;
                }
            }
        }

        static void PassengerRegistration()
        {
            Passenger passenger = new Passenger();
            Console.Clear ();
            Console.WriteLine("===REGISTRACIJA PUTNIKA===");

            while (true)
            {
                Console.Write("Unesite ime: ");
                var name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    passenger.Name = name;
                    break;
                }
                else Console.WriteLine("Prezime ne smije biti prazno, pokusajte ponovno.");
            }

            while (true)
            {
                Console.Write("Unesi prezime: ");
                var surname = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(surname))
                {
                    passenger.Surname = surname;
                    break;
                }
                else Console.WriteLine("Prezime ne smije biti prazno, pokusajte ponovno.");
            }

            DateOnly birthDate;
            while (true)
            {
                Console.Write("Unesi datum rođenja (yyyy-mm-dd): ");
                if (DateOnly.TryParse(Console.ReadLine(), out birthDate))
                {
                    passenger.BirthDate = birthDate;
                    break;
                }
                Console.WriteLine("Pogrešan format datuma!");
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
                passenger.Email = email;
                break;
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
                else
                {
                    passenger.Password = passwordFirst;
                    break;
                }
            }
            InitialData.Passengers.Add(passenger);
            Console.WriteLine($"Putnik {passenger.Name} {passenger.Surname} registriran.");
            Console.WriteLine("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadLine();
        }

    }
}