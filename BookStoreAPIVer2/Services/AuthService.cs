using System.Text.RegularExpressions;
using AutoMapper;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Repository.IRepository;
using BookStoreAPIVer2.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace BookStoreAPIVer2.Services;

public class AuthService  : IAuthServiece
{
    private readonly IAuthRepository _authRepo;
    private readonly PasswordHasher<NhanVien> _passwordHasher;
    private readonly IMapper _mapper;

    public AuthService(IAuthRepository authRepo, IMapper mapper)
    {
        _authRepo = authRepo;
        _passwordHasher = new PasswordHasher<NhanVien>();
        _mapper = mapper;
    }

    public async Task<NhanVienDTO> RegisterEmployee(RegisterEmployeeRequestDTO registerEmployeeRequestDTO)
    {
        
        CheckEmployeeBeforeInsert(registerEmployeeRequestDTO);
        
        NhanVien nv = new()
        {
            HoTen = registerEmployeeRequestDTO.HoTen,
            NgaySinh = registerEmployeeRequestDTO.NgaySinh,
            DiaChi = registerEmployeeRequestDTO.DiaChi,
            ChucVu = registerEmployeeRequestDTO.ChucVu,
            Gmail = registerEmployeeRequestDTO.Gmail,
            Sdt = registerEmployeeRequestDTO.Sdt,
            Luong = registerEmployeeRequestDTO.Luong,
            NgayTaoTaiKhoan = DateTime.UtcNow
        };
        nv.MatKhau = _passwordHasher.HashPassword(nv, registerEmployeeRequestDTO.MatKhau);
        var employee = await _authRepo.RegisterEmployee(nv);
        var userReturn = _mapper.Map<NhanVienDTO>(employee);
        return userReturn;
    }

    public async Task<KhachHangDTO> RegisterCustomer(RegisterCustomerRequestDTO registerCustomerRequestDTO)
    {
        
        CheckCustomerBeforeInsert(registerCustomerRequestDTO);
        
        KhachHang kh = new()
        {
            HoTenKh = registerCustomerRequestDTO.HoTenKh,
            DiaChi = registerCustomerRequestDTO.DiaChi,
            NgaySinh = registerCustomerRequestDTO.NgaySinh,
            GioiTinh = registerCustomerRequestDTO.GioiTinh,
            Sdt = registerCustomerRequestDTO.Sdt,
            NgayTao = DateTime.UtcNow
        };

        var customer = await _authRepo.RegisterCustomer(kh);
        var customerReturn = _mapper.Map<KhachHangDTO>(customer);
        return customerReturn;
    }

    public async Task<TokenDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        
        CheckEmployeeBeforeLogin(loginRequestDTO);
        
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

    public async Task<UpdateNhanVienDTO> UpdateEmployee(UpdateNhanVienDTO updateNhanVienDTO)
    {
        var oldVerEmployee = await _authRepo.GetEmployeeAsync(updateNhanVienDTO.MaTk);
        var nv = new NhanVien()
        {
            MaTk = updateNhanVienDTO.MaTk,
            HoTen = updateNhanVienDTO.HoTen,
            NgaySinh = updateNhanVienDTO.NgaySinh,
            DiaChi = updateNhanVienDTO.DiaChi,
            Gmail = updateNhanVienDTO.Gmail,
            Sdt = updateNhanVienDTO.Sdt,
            ChucVu = updateNhanVienDTO.ChucVu,
            Luong = updateNhanVienDTO.Luong,
            NgayTaoTaiKhoan = DateTime.UtcNow,
            MatKhau = oldVerEmployee.MatKhau
        };
        var result = await _authRepo.UpdateEmployee(nv);
        var employeeReturn = _mapper.Map<UpdateNhanVienDTO>(result);
        return employeeReturn;
    }

    public async Task RemoveEmployee(int id)
    {
        var employee = await _authRepo.GetEmployeeAsync(id);
        await _authRepo.RemoveAsync(employee);
    }

    private void CheckEmployeeBeforeInsert(RegisterEmployeeRequestDTO registerEmployeeRequestDTO)
    {
        if (registerEmployeeRequestDTO.MatKhau.Length < 6)
        {
            throw new Exception("Mật khẩu không đủ độ dài");
        }
        
        if (registerEmployeeRequestDTO.NgaySinh == DateTime.UtcNow || (DateTime.UtcNow.Year - registerEmployeeRequestDTO.NgaySinh.Year) < 18)
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

        if (registerEmployeeRequestDTO.ChucVu != "Quan ly" && registerEmployeeRequestDTO.ChucVu != "Nhan vien")
        {
            throw new Exception("Chức vụ không hợp lý");
        }
        
        var phoneRegex = @"^0\d{9}$";
        if (string.IsNullOrWhiteSpace(registerEmployeeRequestDTO.Sdt) ||
            Regex.IsMatch(registerEmployeeRequestDTO.Sdt, phoneRegex) == false)
        {
            throw new Exception("Số điện thoại không hợp lệ");
        }

        if (registerEmployeeRequestDTO.Luong < 15000)
        {
            throw new Exception("Lương của nhân viên ít nhất là 15000 một giờ");
        }
    }
    
    private void CheckCustomerBeforeInsert(RegisterCustomerRequestDTO registerCustomerRequestDTO)
    {
        if (registerCustomerRequestDTO.NgaySinh == DateTime.UtcNow || (DateTime.UtcNow.Year - registerCustomerRequestDTO.NgaySinh.Year) < 15)
        {
            throw new Exception("Khách hàng phải đủ 15 tuổi");
        }

        if (registerCustomerRequestDTO.GioiTinh != "Nam" && registerCustomerRequestDTO.GioiTinh != "Nu")
        {
            throw new Exception("Giới tính không hợp lý");
        }
        
        var phoneRegex = @"^0\d{9}$";
        if (string.IsNullOrWhiteSpace(registerCustomerRequestDTO.Sdt) ||
            Regex.IsMatch(registerCustomerRequestDTO.Sdt, phoneRegex) == false)
        {
            throw new Exception("Số điện thoại không hợp lệ");
        }
    }

    private void CheckEmployeeBeforeLogin(LoginRequestDTO loginRequestDTO)
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