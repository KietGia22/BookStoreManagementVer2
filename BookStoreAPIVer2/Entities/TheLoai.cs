using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreAPIVer2.Entities;

public class TheLoai
{
    public TheLoai(string tenTheLoai)
    {
        TenTheLoai = tenTheLoai;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaTl { get; set; }
    
    public string TenTheLoai { get; set; }
}