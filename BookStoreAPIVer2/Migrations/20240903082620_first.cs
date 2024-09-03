using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookStoreAPIVer2.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHd = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaKh = table.Column<int>(type: "integer", nullable: false),
                    MaTk = table.Column<int>(type: "integer", nullable: false),
                    NgayTaoHd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TongTienHoaDon = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.MaHd);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKh = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MatKhau = table.Column<string>(type: "text", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GioiTinh = table.Column<string>(type: "text", nullable: false),
                    Sdt = table.Column<string>(type: "text", nullable: false),
                    DiaChi = table.Column<string>(type: "text", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKh);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaTk = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MatKhau = table.Column<string>(type: "text", nullable: false),
                    HoTen = table.Column<string>(type: "text", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DiaChi = table.Column<string>(type: "text", nullable: false),
                    Gmail = table.Column<string>(type: "text", nullable: false),
                    Sdt = table.Column<string>(type: "text", nullable: false),
                    ChucVu = table.Column<string>(type: "text", nullable: false),
                    Luong = table.Column<long>(type: "bigint", nullable: false),
                    NgayTaoTaiKhoan = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.MaTk);
                });

            migrationBuilder.CreateTable(
                name: "NhaPhanPhoi",
                columns: table => new
                {
                    MaNpp = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenNpp = table.Column<string>(type: "text", nullable: false),
                    Sdt = table.Column<string>(type: "text", nullable: false),
                    DiaChi = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaPhanPhoi", x => x.MaNpp);
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhapSach",
                columns: table => new
                {
                    MaPns = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaNpp = table.Column<int>(type: "integer", nullable: false),
                    MaTk = table.Column<int>(type: "integer", nullable: false),
                    NgayNhapSach = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TongTienNhapSach = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhapSach", x => x.MaPns);
                });

            migrationBuilder.CreateTable(
                name: "Sach",
                columns: table => new
                {
                    MaSach = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenSach = table.Column<string>(type: "text", nullable: false),
                    TenTg = table.Column<string>(type: "text", nullable: false),
                    Nxb = table.Column<string>(type: "text", nullable: false),
                    MaTl = table.Column<int>(type: "integer", nullable: false),
                    SlHienCo = table.Column<int>(type: "integer", nullable: false),
                    AnhSach = table.Column<string>(type: "text", nullable: false),
                    GiaTien = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sach", x => x.MaSach);
                });

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    MaTl = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenTheLoai = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoai", x => x.MaTl);
                });

            migrationBuilder.CreateTable(
                name: "ChamCong",
                columns: table => new
                {
                    MaTk = table.Column<int>(type: "integer", nullable: false),
                    BatDauLam = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    KetThuc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SoGioLam = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChamCong", x => new { x.MaTk, x.BatDauLam });
                    table.ForeignKey(
                        name: "FK_ChamCong_NhanVien_MaTk",
                        column: x => x.MaTk,
                        principalTable: "NhanVien",
                        principalColumn: "MaTk",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cthd",
                columns: table => new
                {
                    MaHd = table.Column<int>(type: "integer", nullable: false),
                    MaSach = table.Column<int>(type: "integer", nullable: false),
                    SoLuongMuaTungCuonSach = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cthd", x => new { x.MaHd, x.MaSach });
                    table.ForeignKey(
                        name: "FK_Cthd_HoaDon_MaHd",
                        column: x => x.MaHd,
                        principalTable: "HoaDon",
                        principalColumn: "MaHd",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cthd_Sach_MaSach",
                        column: x => x.MaSach,
                        principalTable: "Sach",
                        principalColumn: "MaSach",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CtpnSach",
                columns: table => new
                {
                    MaPns = table.Column<int>(type: "integer", nullable: false),
                    MaSach = table.Column<int>(type: "integer", nullable: false),
                    SoLuongNhapTungCuonSach = table.Column<int>(type: "integer", nullable: false),
                    GiaSachNhap = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CtpnSach", x => new { x.MaPns, x.MaSach });
                    table.ForeignKey(
                        name: "FK_CtpnSach_PhieuNhapSach_MaPns",
                        column: x => x.MaPns,
                        principalTable: "PhieuNhapSach",
                        principalColumn: "MaPns",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CtpnSach_Sach_MaSach",
                        column: x => x.MaSach,
                        principalTable: "Sach",
                        principalColumn: "MaSach",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cthd_MaSach",
                table: "Cthd",
                column: "MaSach");

            migrationBuilder.CreateIndex(
                name: "IX_CtpnSach_MaSach",
                table: "CtpnSach",
                column: "MaSach");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChamCong");

            migrationBuilder.DropTable(
                name: "Cthd");

            migrationBuilder.DropTable(
                name: "CtpnSach");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhaPhanPhoi");

            migrationBuilder.DropTable(
                name: "TheLoai");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "PhieuNhapSach");

            migrationBuilder.DropTable(
                name: "Sach");
        }
    }
}
