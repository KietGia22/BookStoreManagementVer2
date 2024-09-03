using System.ComponentModel.DataAnnotations;

namespace BookStoreAPIVer2.Entities;

public class TheLoai
{
    public TheLoai(string tenTheLoai)
    {
        TenTheLoai = tenTheLoai;
    }

    [Key]
    public int MaTl { get; set; }
    
    public string TenTheLoai { get; set; }
}