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

            PassengerService.AddReservation(firstReservation, firstPassenger);
            PassengerService.AddReservation(secondReservation, secondPassenger);
            PassengerService.AddReservation(thirdReservation, secondPassenger);

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
    }
}