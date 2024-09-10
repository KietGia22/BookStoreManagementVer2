namespace BookStoreAPIVer2.DTOs;

public class CreateBookDTO
{
    public string BookName { get; set; }
    
    public string Author { get; set; }
    
    public string Publisher { get; set; }
    
    public int CategoryId { get; set; }
    
    public string ImageUrl { get; set; }
    
    public int Quantity { get; set; }
    
    public long Price { get; set; } 
}