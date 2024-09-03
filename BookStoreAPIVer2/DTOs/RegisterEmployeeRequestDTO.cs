namespace BookStoreAPIVer2.DTOs;

public class RegisterEmployeeRequestDTO
{
    public RegisterEmployeeRequestDTO(string matKhau, string hoTen, DateTime ngaySinh, string diaChi, string gmail, string sdt, string chucVu, long luong)
    {
        MatKhau = matKhau;
        HoTen = hoTen;
        NgaySinh = ngaySinh;
        DiaChi = diaChi;
        Gmail = gmail;
        Sdt = sdt;
        ChucVu = chucVu;
        Luong = luong;
    }

    public string MatKhau { get; set; }
        
    public string HoTen { get; set; }
        
    public DateTime NgaySinh { get; set; }
        
    public string DiaChi { get; set; }
        
    public string Gmail { get; set; }
        
    public string Sdt  { get; set; }
        
    public string ChucVu { get; set; }
        
    public long Luong { get; set; }
}