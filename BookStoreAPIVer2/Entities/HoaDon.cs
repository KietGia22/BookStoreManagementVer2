using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class HoaDon
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaHd { get; set; }
    
    [ForeignKey("KhachHang")]
    public int MaKh { get; set; }
    
    [ForeignKey("NhanVien")]
    public int MaTk { get; set; }
    
    public DateTime NgayTaoHd { get; set; }
    
    public long TongTienHoaDon { get; set; }
}