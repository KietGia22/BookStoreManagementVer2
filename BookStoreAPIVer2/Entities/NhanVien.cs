using System.ComponentModel.DataAnnotations;

namespace BookStoreAPIVer2.Entities;

public class NhanVien
{
    public NhanVien(int maTk, string matKhau, string hoTen, string diaChi, string gmail, string sdt, string chucVu, long luong, DateTime ngayTaoTaiKhoan, DateTime ngaySinh)
    {
        MaTk = maTk;
        MatKhau = matKhau;
        HoTen = hoTen;
        DiaChi = diaChi;
        Gmail = gmail;
        Sdt = sdt;
        ChucVu = chucVu;
        Luong = luong;
        NgayTaoTaiKhoan = ngayTaoTaiKhoan;
        NgaySinh = ngaySinh;
    }

    [Key]
    public int MaTk { get; set; }
    
    public string MatKhau { get; set; }
    
    public string HoTen { get; set; }
    
    public DateTime NgaySinh { get; set; }
    
    public string DiaChi { get; set; }
    
    public string Gmail { get; set; }
    
    public string Sdt  { get; set; }
    
    public string ChucVu { get; set; }
    
    public long Luong { get; set; }
    
    public DateTime NgayTaoTaiKhoan  { get; set; }
}