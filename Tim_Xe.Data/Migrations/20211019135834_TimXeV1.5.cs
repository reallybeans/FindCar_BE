using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim_Xe.Data.Migrations
{
    public partial class TimXeV15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "City");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Group",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "Group");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "City",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
