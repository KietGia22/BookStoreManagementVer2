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
   private readonly PasswordHasher<Employee> _passwordHasher;
   internal DbSet<Employee> _employees;
   internal DbSet<Customer> _customers;

   public AuthRepository(ApplicationDbContext db, IConfiguration configuration)
   {
      _db = db;
      secretKey = configuration.GetValue<string>("ApiSettings:Secret");
      _passwordHasher = new PasswordHasher<Employee>();
      this._employees = _db.Set<Employee>();
      this._customers = _db.Set<Customer>();
   }

   public async Task<Employee> GetEmployeeAsync(int id)
   {
      var employee = await _employees.AsNoTracking().Where(u => u.AccID == id).FirstOrDefaultAsync();
      return employee;
   }

   public async Task<Employee> RegisterEmployee(Employee nhanVien)
   {
      await _employees.AddAsync(nhanVien);
      await _db.SaveChangesAsync();
      var userReturn = await _employees.FirstOrDefaultAsync(u => u.Gmail == nhanVien.Gmail);
      return userReturn;
   }

   public async Task<Customer> RegisterCustomer(Customer khachHang)
   {
      await _customers.AddAsync(khachHang);
      await _db.SaveChangesAsync();
      var customerReturn = await _customers.FirstOrDefaultAsync(u => u.CustomerId == khachHang.CustomerId);
      return customerReturn;
   }

   public async Task<string> Login(string gmail, string password)
   {
      var employee = await _employees.FirstOrDefaultAsync(u => u.Gmail == gmail);
      var passwordChecked = _passwordHasher.VerifyHashedPassword(employee, employee.Password, password);
      if (passwordChecked == PasswordVerificationResult.Failed || employee == null)
      {
         throw new Exception("User or password incorrect.");
      }
      var jwtTokenId = $"JTI{Guid.NewGuid()}";
      var accessToken = await GetAccessToken(employee, jwtTokenId);

      return accessToken;
   }

   public async Task<Employee> UpdateEmployee(Employee nhanVien)
   {
      _employees.Update(nhanVien);
      await _db.SaveChangesAsync();
      return nhanVien;
   }

   public async Task RemoveAsync(Employee nhanVien)
   {
      _employees.Remove(nhanVien);
      await _db.SaveChangesAsync();
   }

   public bool IsUniqueUser(string gmail)
   {
      var employee = _employees.FirstOrDefault(u => u.Gmail == gmail);

      if (employee == null)
      {
         return true;
      }

      return false;
   }
   
   private async Task<string> GetAccessToken(Employee nv, string jwtTokenId)
   {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(secretKey);
      
      var tokenDescriptor = new SecurityTokenDescriptor
      {
         Subject = new ClaimsIdentity(new Claim[]
         {
            new Claim(ClaimTypes.Email, nv.Gmail.ToString()),
            new Claim(ClaimTypes.NameIdentifier, nv.AccID.ToString()),
            new Claim(ClaimTypes.Role, nv.Title.ToString())
         }),
         Expires = DateTime.UtcNow.AddDays(7),
         SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenStr = tokenHandler.WriteToken(token);
      return tokenStr;
   }
}