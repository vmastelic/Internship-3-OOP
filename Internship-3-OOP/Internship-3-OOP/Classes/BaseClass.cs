namespace Internship_3_OOP.Classes
{
    public abstract class BaseClass
    {
        public Guid ID { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }

        protected BaseClass()
        {
            ID = Guid.NewGuid();
            CreatedTime = DateTime.Now;
            LastUpdatedTime = DateTime.Now;
        }

        public void UpdateTimestamp()
        {
            LastUpdatedTime = DateTime.Now;
        }
    }
}
