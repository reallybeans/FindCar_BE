using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim_Xe.Data.Migrations
{
    public partial class TimXeV14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "City");

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "City",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "City",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Notificatied",
                table: "Booking_Driver",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartAt",
                table: "Booking",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityName",
                table: "City");

            migrationBuilder.DropColumn(
                name: "District",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Notificatied",
                table: "Booking_Driver");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "City",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartAt",
                table: "Booking",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }
    }
}
