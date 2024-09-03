using System.ComponentModel.DataAnnotations;

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
    public int MaNpp { get; set; }
    
    public string TenNpp { get; set; }
    
    public string Sdt { get; set; }
    
    public string DiaChi { get; set; }
    
}