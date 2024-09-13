using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartId { get; set; }
    
    [ForeignKey("Book")]
    public int BookId { get; set; }
    
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    
    
    public int Quantity { get; set; }
    
    public Customer Customer { get; set; }
    
    public Book Book { get; set; }
}