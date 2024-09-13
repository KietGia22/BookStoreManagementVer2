namespace BookStoreAPIVer2.Entities;

public class OrderDetail
{
    public int InvoiceId { get; set; }
    
    public int BookId { get; set; }
    
    public long Quantity { get; set; }
    
    public long Price { get; set; }
    
    public Book Book { get; set; }
    
    public Order Invoice { get; set; }
}