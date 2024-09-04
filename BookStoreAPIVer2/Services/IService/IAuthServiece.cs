using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2.Services.IService;

public interface IAuthServiece
{
    Task<NhanVienDTO> RegisterEmployee(RegisterEmployeeRequestDTO registerEmployeeRequestDTO);
    
    Task <KhachHangDTO> RegisterCustomer(RegisterCustomerRequestDTO registerCustomerRequestDTO);

    Task<UpdateNhanVienDTO> UpdateEmployee(UpdateNhanVienDTO updateNhanVienDto);

    Task RemoveEmployee(int id);
    
    Task<TokenDTO> Login(LoginRequestDTO loginRequestDTO);
}