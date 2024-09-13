using System.Text.RegularExpressions;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Repository.IRepository;

namespace BookStoreAPIVer2.Helper;

public class CheckEmployeeDTO
{
    public static void CheckEmployeeBeforeInsert(RegisterEmployeeRequestDTO registerEmployeeRequestDTO, IAuthRepository _authRepo)
    {
        if (registerEmployeeRequestDTO.Password.Length < 6)
        {
            throw new Exception("Mật khẩu không đủ độ dài");
        }
        
        if (registerEmployeeRequestDTO.Birthday == DateTime.UtcNow || (DateTime.UtcNow.Year - registerEmployeeRequestDTO.Birthday.Year) < 18)
        {
            throw new Exception("Nhân viên phải đủ 18 tuổi");
        }

        if (!_authRepo.IsUniqueUser(registerEmployeeRequestDTO.Gmail))
        {
            throw new Exception("Employee already exist");
        }
        
        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (string.IsNullOrWhiteSpace(registerEmployeeRequestDTO.Gmail) ||
            Regex.IsMatch(registerEmployeeRequestDTO.Gmail, emailRegex) == false)
        {
            throw new Exception("Email không hợp lệ");
        }

        if (registerEmployeeRequestDTO.Title != "Quan ly" && registerEmployeeRequestDTO.Title != "Nhan vien")
        {
            throw new Exception("Chức vụ không hợp lý");
        }
        
        var phoneRegex = @"^0\d{9}$";
        if (string.IsNullOrWhiteSpace(registerEmployeeRequestDTO.Phone) ||
            Regex.IsMatch(registerEmployeeRequestDTO.Phone, phoneRegex) == false)
        {
            throw new Exception("Số điện thoại không hợp lệ");
        }

        if (registerEmployeeRequestDTO.Salary < 15000)
        {
            throw new Exception("Lương của nhân viên ít nhất là 15000 một giờ");
        }
    }
    
    public async static void CheckEmployeeBeforeLogin(LoginRequestDTO loginRequestDTO, IAuthRepository _authRepo)
    {
        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (string.IsNullOrWhiteSpace(loginRequestDTO.Gmail) ||
            Regex.IsMatch(loginRequestDTO.Gmail, emailRegex) == false)
        {
            throw new Exception("Email không hợp lệ");
        }
        
        if (_authRepo.IsUniqueUser(loginRequestDTO.Gmail))
        {
            throw new Exception("Employee doesn't exist");
        }
        
    }
}