using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStoreAPIVer2.Data;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreAPIVer2.Repository;

public class AuthRepository : IAuthRepository
{
   private readonly ApplicationDbContext _db;
   private string secretKey;
   private readonly PasswordHasher<NhanVien> _passwordHasher;
   internal DbSet<NhanVien> _nhanViens;
   internal DbSet<KhachHang> _khachHangs;

   public AuthRepository(ApplicationDbContext db, IConfiguration configuration)
   {
      _db = db;
      secretKey = configuration.GetValue<string>("ApiSettings:Secret");
      _passwordHasher = new PasswordHasher<NhanVien>();
      this._nhanViens = _db.Set<NhanVien>();
      this._khachHangs = _db.Set<KhachHang>();
   }

   public async Task<NhanVien> RegisterEmployee(NhanVien nhanVien)
   {
      await _nhanViens.AddAsync(nhanVien);
      await _db.SaveChangesAsync();
      var userReturn = await _nhanViens.FirstOrDefaultAsync(u => u.Gmail == nhanVien.Gmail);
      return userReturn;
   }

   public async Task<string> Login(string gmail, string password)
   {
      var employee = await _nhanViens.FirstOrDefaultAsync(u => u.Gmail == gmail);
      var passwordChecked = _passwordHasher.VerifyHashedPassword(employee, employee.MatKhau, password);
      if (passwordChecked == PasswordVerificationResult.Failed || employee == null)
      {
         throw new Exception("User or password incorrect.");
      }
      var jwtTokenId = $"JTI{Guid.NewGuid()}";
      var accessToken = await GetAccessToken(employee, jwtTokenId);

      return accessToken;
   }

   private async Task<string> GetAccessToken(NhanVien nv, string jwtTokenId)
   {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(secretKey);
      
      var tokenDescriptor = new SecurityTokenDescriptor
      {
         Subject = new ClaimsIdentity(new Claim[]
         {
            new Claim(ClaimTypes.Email, nv.Gmail.ToString()),
            new Claim(ClaimTypes.NameIdentifier, nv.MaTk.ToString()),
            new Claim(ClaimTypes.Name, nv.HoTen.ToString())
         }),
         Expires = DateTime.UtcNow.AddDays(7),
         SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenStr = tokenHandler.WriteToken(token);
      return tokenStr;
   }
}