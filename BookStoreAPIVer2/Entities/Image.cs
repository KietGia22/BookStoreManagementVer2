using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class Image
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ImageId { get; set; }
    
    [ForeignKey("Book")]
    public int BookId { get; set; }
    
    public string ImageUrl { get; set; }
    
    public Book Book { get; set; }
}