using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2.Services.IService;

public interface IAuthServiece
{
    Task<EmployeeDTO> RegisterEmployee(RegisterEmployeeRequestDTO registerEmployeeRequestDTO);
    
    Task <CustomerDTO> RegisterCustomer(RegisterCustomerRequestDTO registerCustomerRequestDTO);

    Task<UpdateEmployeeDTO> UpdateEmployee(UpdateEmployeeDTO updateNhanVienDto);

    Task RemoveEmployee(int id);
    
    Task<TokenDTO> Login(LoginRequestDTO loginRequestDTO);
}