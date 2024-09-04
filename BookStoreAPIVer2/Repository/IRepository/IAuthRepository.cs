using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2.Repository.IRepository;

public interface IAuthRepository
{
    Task<NhanVien> GetEmployeeAsync(int id);
    
    Task<string> Login (string gmail, string passWord);
    
    Task<NhanVien> RegisterEmployee (NhanVien nhanVien);
    
    Task<KhachHang> RegisterCustomer (KhachHang khachHang);
    
    Task<NhanVien> UpdateEmployee (NhanVien nhanVien);

    Task RemoveAsync(NhanVien nhanVien);
    
    public bool IsUniqueUser(string gmail);

}