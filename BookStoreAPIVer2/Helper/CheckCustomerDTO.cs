using System.Text.RegularExpressions;
using BookStoreAPIVer2.DTOs;

namespace BookStoreAPIVer2.Helper;

public class CheckCustomerDTO
{
    public static void CheckCustomerBeforeInsert(RegisterCustomerRequestDTO registerCustomerRequestDTO)
    {
        if (registerCustomerRequestDTO.Birthday == DateTime.UtcNow || (DateTime.UtcNow.Year - registerCustomerRequestDTO.Birthday.Year) < 15)
        {
            throw new Exception("Khách hàng phải đủ 15 tuổi");
        }

        if (registerCustomerRequestDTO.Gender != "Nam" && registerCustomerRequestDTO.Gender != "Nu")
        {
            throw new Exception("Giới tính không hợp lý");
        }
        
        var phoneRegex = @"^0\d{9}$";
        if (string.IsNullOrWhiteSpace(registerCustomerRequestDTO.Phone) ||
            Regex.IsMatch(registerCustomerRequestDTO.Phone, phoneRegex) == false)
        {
            throw new Exception("Số điện thoại không hợp lệ");
        }
    }
}