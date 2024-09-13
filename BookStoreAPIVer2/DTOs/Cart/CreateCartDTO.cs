namespace BookStoreAPIVer2.DTOs.Cart;

public class CreateCartDTO
{
    public int BookId { get; set; }
    
    public int CustomerId { get; set; }

    public int Quantity { get; set; }
}