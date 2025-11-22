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
        
    }
}