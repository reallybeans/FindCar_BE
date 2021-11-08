using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim_Xe.Data.Migrations
{
    public partial class TimXeV37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Booking",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdVehicle",
                table: "Booking",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "IdVehicle",
                table: "Booking");
        }
    }
}
