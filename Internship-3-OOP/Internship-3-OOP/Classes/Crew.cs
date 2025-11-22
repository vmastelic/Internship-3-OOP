using Internship_3_OOP.Classes;

namespace Internship_3_OOP.Classes
{
    public class Crew : BaseClass
    {
        public string Name { get; set; }
        public CrewMember Pilot { get; set; }
        public CrewMember Copilot { get; set; }
        public CrewMember Stewardess1 { get; set; }
        public CrewMember Stewardess2 { get; set; }
        public bool IsAvailable { get; set; } = true;
        public Crew() { }
        public Crew(string name, CrewMember pilot, CrewMember copilot, CrewMember stewardess1, CrewMember stewardess2)
        {
            Name = name;
            Pilot = pilot;
            Copilot = copilot;
            Stewardess2 = stewardess2;
            Stewardess1 = stewardess1;
        }

    }
}
