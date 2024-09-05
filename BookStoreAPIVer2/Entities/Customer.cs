using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class Customer
{
    
    public Customer() {}

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }
    
    public string Name { get; set; }
    
    public DateTime Birthday { get; set; }
    
    public string Gender { get; set; }
    
    public string Phone { get; set; }
    
    public string Address { get; set; }
    
    public DateTime CreateDate { get; set; }
}