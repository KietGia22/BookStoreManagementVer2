using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookId { get; set; }
    
    public string BookName { get; set; }
    
    public string Author { get; set; }
    
    public string Publisher { get; set; }
    
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    
    public int Quantity { get; set; }

    public long Price { get; set; }

    public Category Category { get; set; }
}