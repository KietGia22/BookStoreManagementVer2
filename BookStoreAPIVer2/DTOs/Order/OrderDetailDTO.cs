namespace BookStoreAPIVer2.DTOs;

public class OrderDetailDTO
{
    public int InvoiceId { get; set; }
    
    public int BookId { get; set; }
    
    public long Quantity { get; set; }

    public long Price { get; set; }
}