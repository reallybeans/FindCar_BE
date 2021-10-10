using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim_Xe.Data.Migrations
{
    public partial class TimeXeV10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    expiry_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdDriver = table.Column<int>(type: "int", nullable: true),
                    IdManager = table.Column<int>(type: "int", nullable: true),
                    token = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Driver",
                        column: x => x.IdDriver,
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Account",
                        column: x => x.IdManager,
                        principalTable: "Manager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_IdDriver",
                table: "RefreshToken",
                column: "IdDriver");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_IdManager",
                table: "RefreshToken",
                column: "IdManager");
        }
    }
}
