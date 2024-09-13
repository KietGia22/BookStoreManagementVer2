namespace BookStoreAPIVer2.DTOs;

public class OrderDTO
{
    public int CustomerId { get; set; }
    
    public int AccId { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    
    public long Total { get; set; }
}