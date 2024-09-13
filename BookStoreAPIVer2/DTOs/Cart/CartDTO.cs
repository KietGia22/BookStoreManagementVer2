namespace BookStoreAPIVer2.DTOs.Cart;

public class CartDTO
{
    public int CartId { get; set; }
    
    public int BookId { get; set; }
    
    public int CustomerId { get; set; }

    public int Quantity { get; set; }
}