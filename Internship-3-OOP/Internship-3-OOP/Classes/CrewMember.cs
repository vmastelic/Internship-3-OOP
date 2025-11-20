namespace Internship_3_OOP.Classes
{
    public enum Gender
    {
        Male,
        Female
    }
    public enum Position
    {
        Pilot,
        Copilot,
        Stewardess
    }
    public class CrewMember : BaseClass
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public Position Position { get; set; }
        public DateOnly BirthDate { get; set; }
        public bool HasCrew { get; set; } = false;
        
        public CrewMember(string name, string surname, Gender gender, Position position, DateOnly birthDate)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
            Position = position;
            BirthDate = birthDate;
        }
    }
}
