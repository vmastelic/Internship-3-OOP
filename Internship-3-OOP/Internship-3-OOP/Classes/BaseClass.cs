namespace Internship_3_OOP.Classes
{
    public abstract class BaseClass
    {
        private static int NextID = 0;
        public int ID { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }

        protected BaseClass()
        {
            ID = NextID++;
            CreatedTime = DateTime.Now;
            LastUpdatedTime = DateTime.Now;
        }
        public void UpdateTimestamp()
        {
            LastUpdatedTime = DateTime.Now;
        }
    }
}
