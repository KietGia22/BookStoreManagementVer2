using System.ComponentModel.DataAnnotations;

namespace BookStoreAPIVer2.Entities;

public class KhachHang
{
    public KhachHang(int maKh, string matKhau, DateTime ngaySinh, string gioiTinh, string sdt, string diaChi, DateTime ngayTao)
    {
        MaKh = maKh;
        MatKhau = matKhau;
        NgaySinh = ngaySinh;
        GioiTinh = gioiTinh;
        Sdt = sdt;
        DiaChi = diaChi;
        NgayTao = ngayTao;
    }

    [Key]
    public int MaKh { get; set; }
    
    public string MatKhau { get; set; }
    
    public DateTime NgaySinh { get; set; }
    
    public string GioiTinh { get; set; }
    
    public string Sdt { get; set; }
    
    public string DiaChi { get; set; }
    
    public DateTime NgayTao { get; set; }
}