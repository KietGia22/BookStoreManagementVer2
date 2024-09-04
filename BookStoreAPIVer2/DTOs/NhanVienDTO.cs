namespace BookStoreAPIVer2.DTOs;

public class NhanVienDTO
{
    public NhanVienDTO(int maTk, string hoTen, DateTime ngaySinh, string diaChi, string gmail, string sdt, string chucVu, long luong, DateTime ngayTaoTaiKhoan)
    {
        MaTk = maTk;
        HoTen = hoTen;
        NgaySinh = ngaySinh;
        DiaChi = diaChi;
        Gmail = gmail;
        Sdt = sdt;
        ChucVu = chucVu;
        Luong = luong;
        NgayTaoTaiKhoan = ngayTaoTaiKhoan;
    }

    public int MaTk { get; set; }

    public string HoTen { get; set; }
    
    public DateTime NgaySinh { get; set; }
    
    public string DiaChi { get; set; }
    
    public string Gmail { get; set; }
    
    public string Sdt  { get; set; }
    
    public string ChucVu { get; set; }
    
    public long Luong { get; set; }
    
    public DateTime NgayTaoTaiKhoan  { get; set; }
}