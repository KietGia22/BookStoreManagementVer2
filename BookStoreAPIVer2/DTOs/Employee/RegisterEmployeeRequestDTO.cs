namespace BookStoreAPIVer2.DTOs;

public class RegisterEmployeeRequestDTO
{
    public string Password { get; set; }
        
    public string Name { get; set; }
        
    public DateTime Birthday { get; set; }
        
    public string Address { get; set; }
        
    public string Gmail { get; set; }
        
    public string Phone  { get; set; }
        
    public string Title { get; set; }
        
    public long Salary { get; set; }
}