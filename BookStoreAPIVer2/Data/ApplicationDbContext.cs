using BookStoreAPIVer2.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPIVer2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<TheLoai> TheLoai { get; set; }
        public DbSet<Sach> Sach { get; set; }
        public DbSet<NhaPhanPhoi> NhaPhanPhoi { get; set; }
        public DbSet<PhieuNhapSach> PhieuNhapSach { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<Cthd> Cthd { get; set; }
        public DbSet<CtpnSach> CtpnSach { get; set; }
        public DbSet<ChamCong> ChamCong { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChamCong>().HasKey(c => new { c.MaTk, c.BatDauLam });

            modelBuilder.Entity<CtpnSach>().HasKey(c => new { c.MaPns, c.MaSach });

            modelBuilder.Entity<Cthd>().HasKey(c => new { c.MaHd, c.MaSach });

            modelBuilder.Entity<ChamCong>().HasOne(c => c.NhanVien).WithMany().HasForeignKey(c => c.MaTk);

            modelBuilder.Entity<CtpnSach>().HasOne(c => c.PhieuNhapSach).WithMany().HasForeignKey(c => c.MaPns);

            modelBuilder.Entity<CtpnSach>().HasOne(c => c.Sach).WithMany().HasForeignKey(c => c.MaSach);

            modelBuilder.Entity<Cthd>().HasOne(c => c.Sach).WithMany().HasForeignKey(c => c.MaSach);

            modelBuilder.Entity<Cthd>().HasOne(c => c.HoaDon).WithMany().HasForeignKey(c => c.MaHd);

            base.OnModelCreating((modelBuilder));
        }
    }
}
