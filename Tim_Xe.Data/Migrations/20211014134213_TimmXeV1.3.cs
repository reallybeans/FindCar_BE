using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim_Xe.Data.Migrations
{
    public partial class TimmXeV13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Driver_Booking",
                table: "Booking_Driver");

            migrationBuilder.DropForeignKey(
                name: "FK_Point_Trip",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_IdTrip",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking_Driver",
                table: "Booking_Driver");

            migrationBuilder.DropColumn(
                name: "IdTrip",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "IdTrip",
                table: "Booking_Driver");

            migrationBuilder.AddColumn<int>(
                name: "IdBooking",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdBooking",
                table: "Booking_Driver",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking_Driver",
                table: "Booking_Driver",
                columns: new[] { "IdBooking", "IdDriver" });

            migrationBuilder.CreateIndex(
                name: "IX_Location_IdBooking",
                table: "Location",
                column: "IdBooking");

            migrationBuilder.CreateIndex(
                name: "IX_Group_IdManager",
                table: "Group",
                column: "IdManager");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Driver_Booking",
                table: "Booking_Driver",
                column: "IdBooking",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Manager",
                table: "Group",
                column: "IdManager",
                principalTable: "Manager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Point_Trip",
                table: "Location",
                column: "IdBooking",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Driver_Booking",
                table: "Booking_Driver");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Manager",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Point_Trip",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_IdBooking",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Group_IdManager",
                table: "Group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking_Driver",
                table: "Booking_Driver");

            migrationBuilder.DropColumn(
                name: "IdBooking",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "IdBooking",
                table: "Booking_Driver");

            migrationBuilder.AddColumn<int>(
                name: "IdTrip",
                table: "Location",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTrip",
                table: "Booking_Driver",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking_Driver",
                table: "Booking_Driver",
                columns: new[] { "IdTrip", "IdDriver" });

            migrationBuilder.CreateIndex(
                name: "IX_Location_IdTrip",
                table: "Location",
                column: "IdTrip");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Driver_Booking",
                table: "Booking_Driver",
                column: "IdTrip",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Point_Trip",
                table: "Location",
                column: "IdTrip",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
