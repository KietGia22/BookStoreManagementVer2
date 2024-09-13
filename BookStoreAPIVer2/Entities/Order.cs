using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }
    
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    
    [ForeignKey("Employee")]
    public int AccId { get; set; }
    
    
    public DateTime CreateDate { get; set; }
    
    public long Total { get; set; }
    
    public Customer Customer { get; set; }
    
    public Employee Employee { get; set; }
}