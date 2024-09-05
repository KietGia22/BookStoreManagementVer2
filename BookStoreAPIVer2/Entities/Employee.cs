using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class Employee
{
    public Employee(){}

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccID { get; set; }
    
    public string Password { get; set; }
    
    public string Name { get; set; }
    
    public DateTime Birthday { get; set; }
    
    public string Address { get; set; }
    
    public string Gmail { get; set; }
    
    public string Phone  { get; set; }
    
    public string Title { get; set; }
    
    public long Salary { get; set; }
    
    public DateTime CreateDate  { get; set; }
}