using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTPTPMQL.Migrations
{
    /// <inheritdoc />
    public partial class Create_table_Daily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "HeThongPhanPhoi",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DienThoai",
                table: "HeThongPhanPhoi",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "HeThongPhanPhoi",
                type: "TEXT",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaDaiLy",
                table: "HeThongPhanPhoi",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiDaiDien",
                table: "HeThongPhanPhoi",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenDaiLy",
                table: "HeThongPhanPhoi",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "HeThongPhanPhoi");

            migrationBuilder.DropColumn(
                name: "DienThoai",
                table: "HeThongPhanPhoi");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "HeThongPhanPhoi");

            migrationBuilder.DropColumn(
                name: "MaDaiLy",
                table: "HeThongPhanPhoi");

            migrationBuilder.DropColumn(
                name: "NguoiDaiDien",
                table: "HeThongPhanPhoi");

            migrationBuilder.DropColumn(
                name: "TenDaiLy",
                table: "HeThongPhanPhoi");
        }
    }
}
