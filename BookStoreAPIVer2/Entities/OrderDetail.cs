using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class OrderDetail
{
    [ForeignKey("Order")]
    public int InvoiceId { get; set; }
    
    [ForeignKey("Book")]
    public int BookId { get; set; }
    
    public long Quantity { get; set; }
    
    public long Price { get; set; }
    
    public Book Book { get; set; }
    
    public Order Invoice { get; set; }
}