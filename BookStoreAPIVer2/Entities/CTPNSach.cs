using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class CtpnSach
{
    public int MaPns { get; set; }
    
    public int MaSach { get; set; }
    
    public int SoLuongNhapTungCuonSach { get; set; }
    
    public long GiaSachNhap { get; set; }
    
    public PhieuNhapSach PhieuNhapSach { get; set; }
    
    public Sach Sach { get; set; }
}