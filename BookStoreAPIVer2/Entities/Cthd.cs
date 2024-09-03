using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStoreAPIVer2.Entities;

public class Cthd
{
    public int MaHd { get; set; }
    
    public int MaSach { get; set; }
    
    public long SoLuongMuaTungCuonSach { get; set; }
    
    public Sach Sach { get; set; }
    
    public HoaDon HoaDon { get; set; }
}