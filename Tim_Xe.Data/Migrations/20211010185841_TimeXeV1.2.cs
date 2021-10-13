using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim_Xe.Data.Migrations
{
    public partial class TimeXeV12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VehicleType",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PriceTime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PriceKm",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "News",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VehicleType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PriceTime");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PriceKm");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "News");
        }
    }
}
