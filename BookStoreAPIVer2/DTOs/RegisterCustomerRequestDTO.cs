namespace BookStoreAPIVer2.DTOs;

public class RegisterCustomerRequestDTO
{
    public RegisterCustomerRequestDTO(string hoTenKh, DateTime ngaySinh, string gioiTinh, string sdt, string diaChi)
    {
        HoTenKh = hoTenKh;
        NgaySinh = ngaySinh;
        GioiTinh = gioiTinh;
        Sdt = sdt;
        DiaChi = diaChi;
    }

    public string HoTenKh { get; set; }
    
    public DateTime NgaySinh { get; set; }
    
    public string GioiTinh { get; set; }
    
    public string Sdt { get; set; }
    
    public string DiaChi { get; set; }
}