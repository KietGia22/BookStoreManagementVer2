using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities
{
    public class TimeKeeping
    {
        [ForeignKey("Employee")]
        public int AccId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double TotalTime { get; set; }

        public Employee Employee { get; set; }
    }
}
