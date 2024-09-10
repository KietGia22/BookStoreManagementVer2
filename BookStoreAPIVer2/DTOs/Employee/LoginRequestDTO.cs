namespace BookStoreAPIVer2.DTOs;

public class LoginRequestDTO
{
    public LoginRequestDTO(string gmail, string password)
    {
        Gmail = gmail;
        Password = password;
    }

    public string Gmail { get; set; }
    
    public string Password { get; set; }
}