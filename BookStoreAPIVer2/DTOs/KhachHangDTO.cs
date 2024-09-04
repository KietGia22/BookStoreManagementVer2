namespace BookStoreAPIVer2.DTOs;

public class KhachHangDTO
{
    
    public KhachHangDTO(){}
    
    public KhachHangDTO(string hoTenKh, DateTime ngaySinh, string gioiTinh, string sdt, string diaChi, DateTime ngayTao)
    {
        HoTenKh = hoTenKh;
        NgaySinh = ngaySinh;
        GioiTinh = gioiTinh;
        Sdt = sdt;
        DiaChi = diaChi;
        NgayTao = ngayTao;
    }

    public string HoTenKh { get; set; }
    
    public DateTime NgaySinh { get; set; }
    
    public string GioiTinh { get; set; }
    
    public string Sdt { get; set; }
    
    public string DiaChi { get; set; }
    
    public DateTime NgayTao { get; set; }
}