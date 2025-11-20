namespace Internship_3_OOP.Classes
{
    public class Airplane : BaseClass
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int StandardCapacity { get; set; }
        public int BusinessCapacity { get; set; }
        public int VipCapacity { get; set; }
        public int FlightsNumber { get; set; } = 0;
        public Airplane() { }
        public Airplane(string name, int year, int standardCapacity, int buisnessCapacity, int vipCapacity)
        {
            Name = name;
            Year = year;
            StandardCapacity = standardCapacity;
            BusinessCapacity = buisnessCapacity;
            VipCapacity = vipCapacity;
        }
    }
}
