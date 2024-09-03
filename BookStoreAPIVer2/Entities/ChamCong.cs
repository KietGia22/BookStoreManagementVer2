namespace BookStoreAPIVer2.Entities
{
    public class ChamCong
    {
        public int MaTk { get; set; }

        public DateTime BatDauLam { get; set; }

        public DateTime KetThuc { get; set; }

        public int SoGioLam { get; set; }

        public NhanVien NhanVien { get; set; }
    }
}
