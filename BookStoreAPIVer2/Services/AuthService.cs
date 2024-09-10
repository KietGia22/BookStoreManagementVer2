using System.Text.RegularExpressions;
using AutoMapper;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Helper;
using BookStoreAPIVer2.Repository.IRepository;
using BookStoreAPIVer2.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace BookStoreAPIVer2.Services;

public class AuthService  : IAuthServiece
{
    private readonly IAuthRepository _authRepo;
    private readonly PasswordHasher<Employee> _passwordHasher;
    private readonly IMapper _mapper;

    public AuthService(IAuthRepository authRepo, IMapper mapper)
    {
        _authRepo = authRepo;
        _passwordHasher = new PasswordHasher<Employee>();
        _mapper = mapper;
    }

    public async Task<EmployeeDTO> RegisterEmployee(RegisterEmployeeRequestDTO registerEmployeeRequestDTO)
    {
        
        CheckEmployeeDTO.CheckEmployeeBeforeInsert(registerEmployeeRequestDTO, _authRepo);
        
        Employee nv = new()
        {
            Name = registerEmployeeRequestDTO.Name,
            Birthday = registerEmployeeRequestDTO.Birthday,
            Address = registerEmployeeRequestDTO.Address,
            Title = registerEmployeeRequestDTO.Title,
            Gmail = registerEmployeeRequestDTO.Gmail,
            Phone = registerEmployeeRequestDTO.Phone,
            Salary = registerEmployeeRequestDTO.Salary,
            CreateDate = DateTime.UtcNow
        };
        nv.Password = _passwordHasher.HashPassword(nv, registerEmployeeRequestDTO.Password);
        var employee = await _authRepo.RegisterEmployee(nv);
        var userReturn = _mapper.Map<EmployeeDTO>(employee);
        return userReturn;
    }

    public async Task<CustomerDTO> RegisterCustomer(RegisterCustomerRequestDTO registerCustomerRequestDTO)
    {
        
        CheckCustomerDTO.CheckCustomerBeforeInsert(registerCustomerRequestDTO);
        
        Customer kh = new()
        {
            Name = registerCustomerRequestDTO.Name,
            Address = registerCustomerRequestDTO.Address,
            Birthday = registerCustomerRequestDTO.Birthday,
            Gender = registerCustomerRequestDTO.Gender,
            Phone = registerCustomerRequestDTO.Phone,
            CreateDate = DateTime.UtcNow
        };

        var customer = await _authRepo.RegisterCustomer(kh);
        var customerReturn = _mapper.Map<CustomerDTO>(customer);
        return customerReturn;
    }

    public async Task<TokenDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        
        CheckEmployeeDTO.CheckEmployeeBeforeLogin(loginRequestDTO, _authRepo);
        
        var tokenReturn = await _authRepo.Login(loginRequestDTO.Gmail, loginRequestDTO.Password);

        if (tokenReturn == null)
        {
            throw new Exception("Login failed");
        }

        var tokenDto = new TokenDTO
        {
            AccessToken = tokenReturn
        };
        return tokenDto;
    }

    public async Task<UpdateEmployeeDTO> UpdateEmployee(UpdateEmployeeDTO updateNhanVienDTO)
    {
        var oldVerEmployee = await _authRepo.GetEmployeeAsync(updateNhanVienDTO.AccID);
        var nv = new Employee()
        {
            AccID = updateNhanVienDTO.AccID,
            Name = updateNhanVienDTO.Name,
            Birthday = updateNhanVienDTO.Birthday,
            Address = updateNhanVienDTO.Address,
            Gmail = updateNhanVienDTO.Gmail,
            Phone = updateNhanVienDTO.Phone,
            Title = updateNhanVienDTO.Title,
            Salary = updateNhanVienDTO.Salary,
            CreateDate = DateTime.UtcNow,
            Password = oldVerEmployee.Password
        };
        var result = await _authRepo.UpdateEmployee(nv);
        var employeeReturn = _mapper.Map<UpdateEmployeeDTO>(result);
        return employeeReturn;
    }

    public async Task RemoveEmployee(int id)
    {
        var employee = await _authRepo.GetEmployeeAsync(id);
        await _authRepo.RemoveAsync(employee);
    }
}