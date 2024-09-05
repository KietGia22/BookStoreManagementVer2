using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class Invoice
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InvoiceId { get; set; }
    
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    
    [ForeignKey("Employee")]
    public int AccId { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public long Total { get; set; }
}