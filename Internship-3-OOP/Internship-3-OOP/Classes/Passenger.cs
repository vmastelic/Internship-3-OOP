namespace Internship_3_OOP.Classes
{
    public class Passenger : BaseClass
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Flight> Flights { get; set; } = new List<Flight>();
    }
}
