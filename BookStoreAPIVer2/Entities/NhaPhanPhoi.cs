using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class NhaPhanPhoi
{
    public NhaPhanPhoi(int maNpp, string tenNpp, string sdt, string diaChi)
    {
        MaNpp = maNpp;
        TenNpp = tenNpp;
        Sdt = sdt;
        DiaChi = diaChi;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaNpp { get; set; }
    
    public string TenNpp { get; set; }
    
    public string Sdt { get; set; }
    
    public string DiaChi { get; set; }
    
}