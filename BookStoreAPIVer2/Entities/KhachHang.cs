using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class KhachHang
{
    public KhachHang(int maKh, DateTime ngaySinh, string gioiTinh, string sdt, string diaChi, DateTime ngayTao)
    {
        MaKh = maKh;
        NgaySinh = ngaySinh;
        GioiTinh = gioiTinh;
        Sdt = sdt;
        DiaChi = diaChi;
        NgayTao = ngayTao;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaKh { get; set; }
    
    public DateTime NgaySinh { get; set; }
    
    public string GioiTinh { get; set; }
    
    public string Sdt { get; set; }
    
    public string DiaChi { get; set; }
    
    public DateTime NgayTao { get; set; }
}