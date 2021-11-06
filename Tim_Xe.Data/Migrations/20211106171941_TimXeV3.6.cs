using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim_Xe.Data.Migrations
{
    public partial class TimXeV36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Vehicle",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Vehicle");
        }
    }
}
