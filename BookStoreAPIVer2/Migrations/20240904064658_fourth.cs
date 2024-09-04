using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAPIVer2.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HoTenKh",
                table: "KhachHang",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoTenKh",
                table: "KhachHang");
        }
    }
}
