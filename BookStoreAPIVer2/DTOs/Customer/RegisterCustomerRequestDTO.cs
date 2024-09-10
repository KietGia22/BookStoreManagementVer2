namespace BookStoreAPIVer2.DTOs;

public class RegisterCustomerRequestDTO
{
    public string Name { get; set; }
    
    public DateTime Birthday { get; set; }
    
    public string Gender { get; set; }
    
    public string Phone { get; set; }
    
    public string Address { get; set; }
}