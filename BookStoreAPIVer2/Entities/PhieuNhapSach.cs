using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class PhieuNhapSach
{
    public PhieuNhapSach(DateTime ngayNhapSach)
    {
        NgayNhapSach = ngayNhapSach;
    }

    [Key]
    public int MaPns {get; set;}
    
    [ForeignKey("NhaPhanPhoi")]
    public int MaNpp {get; set;}
    
    [ForeignKey("NhanVien")]
    public int MaTk {get; set;}
    
    public DateTime NgayNhapSach {get; set;}
    
    public long TongTienNhapSach {get; set;}
}