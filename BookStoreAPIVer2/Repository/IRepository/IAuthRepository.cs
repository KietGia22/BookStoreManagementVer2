using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2.Repository.IRepository;

public interface IAuthRepository
{
    Task<Employee> GetEmployeeAsync(int id);
    
    Task<string> Login (string gmail, string passWord);
    
    Task<Employee> RegisterEmployee (Employee nhanVien);
    
    Task<Customer> RegisterCustomer (Customer khachHang);
    
    Task<Employee> UpdateEmployee (Employee nhanVien);

    Task RemoveAsync(Employee nhanVien);
    
    public bool IsUniqueUser(string gmail);

}