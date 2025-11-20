namespace Internship_3_OOP.Classes
{
    public class Airplane : BaseClass
    {
        public string Name { get; set; }
        public int Year { get; set; }

        public Airplane(string name, int year)
        {
            Name = name;
            Year = year;
        }
    }
}
