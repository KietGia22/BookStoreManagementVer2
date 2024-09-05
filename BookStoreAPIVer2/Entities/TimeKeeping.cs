namespace BookStoreAPIVer2.Entities
{
    public class TimeKeeping
    {
        public int AccId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int TotalTime { get; set; }

        public Employee Employee { get; set; }
    }
}
