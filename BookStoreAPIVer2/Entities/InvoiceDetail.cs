using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStoreAPIVer2.Entities;

public class InvoiceDetail
{
    public int InvoiceId { get; set; }
    
    public int BookId { get; set; }
    
    public long Quantity { get; set; }
    
    public Book Book { get; set; }
    
    public Invoice Invoice { get; set; }
}