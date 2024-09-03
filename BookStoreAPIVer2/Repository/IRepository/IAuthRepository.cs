using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2.Repository.IRepository;

public interface IAuthRepository
{
    //public bool IsUniqueUser(string gmail);
    
    //Task<string> Login (string gmail, string passWord);
    
    Task<NhanVien> RegisterEmployee (NhanVien nhanVien);
    
    //Task<KhachHang> RegisterCustomer (KhachHang khachHang);
}