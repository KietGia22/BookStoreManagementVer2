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

    public async Task<TokenDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        var tokenReturn = await _authRepo.Login(loginRequestDTO.Gmail, loginRequestDTO.Password);

        if (tokenReturn == null)
        {
            throw new Exception("Login failed");
        }

        TokenDTO tokenDto = new TokenDTO
        {
            AccessToken = tokenReturn
        };
        return tokenDto;
    }
}