using BookStoreAPIVer2.Data;
using BookStoreAPIVer2.Entities;
using BookStoreAPIVer2.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPIVer2.Repository;

public class AuthRepository : IAuthRepository
{
   private readonly ApplicationDbContext _db;
   private readonly PasswordHasher<NhanVien> _passwordHasher;
   internal DbSet<NhanVien> _nhanViens;
   internal DbSet<KhachHang> _khachHangs;

   public AuthRepository(ApplicationDbContext db)
   {
      _db = db;
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

}