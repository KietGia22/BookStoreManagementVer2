using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class Sach
{
    public Sach(int maSach, string tenSach, string tenTg, string nxb, int maTl, int slHienCo, string anhSach, long giaTien)
    {
        MaSach = maSach;
        TenSach = tenSach;
        TenTg = tenTg;
        Nxb = nxb;
        MaTl = maTl;
        SlHienCo = slHienCo;
        AnhSach = anhSach;
        GiaTien = giaTien;
    }

    [Key]
    public int MaSach { get; set; }
    
    public string TenSach { get; set; }
    
    public string TenTg { get; set; }
    
    public string Nxb { get; set; }
    
    [ForeignKey("TheLoai")]
    public int MaTl { get; set; }
    
    public int SlHienCo { get; set; }
    
    public string AnhSach { get; set; }
    
    public long GiaTien { get; set; }
}