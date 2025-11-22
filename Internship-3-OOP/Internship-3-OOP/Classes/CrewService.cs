
namespace Internship_3_OOP.Classes
{
    public class CrewService
    {
        public static void PrintAllCrews()
        {
            Console.Clear();
            Console.WriteLine("Popis svih posada: ");
            foreach (var crew in InitialData.Crews)
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

            InitialData.Crews.Add(newCrew);
            
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
            var available = InitialData.FreeCrewMembers.Where(m => m.Position == position).ToList();
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
            InitialData.FreeCrewMembers.Remove(chosen);
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
            InitialData.FreeCrewMembers.Add(crewMember);
            Console.WriteLine($"\nČlan posade '{crewMember.Name} {crewMember.Surname}' uspješno dodan kao {crewMember.Position}.");
            Console.Write("Pritisnite bilo koju tipku za nastavak...");
            Console.ReadKey();
        }
    }
}
