namespace BookStoreAPIVer2.DTOs.TimeKeeping;

public class TimeKeepingDTO
{
    public int AccId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int TotalTime { get; set; }
}