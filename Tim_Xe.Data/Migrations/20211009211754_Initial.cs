using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim_Xe.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 12, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    img = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameType = table.Column<string>(maxLength: 50, nullable: true),
                    Note = table.Column<string>(maxLength: 200, nullable: true),
                    NumOfSeat = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    IdManager = table.Column<int>(nullable: false),
                    IdCity = table.Column<int>(nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    PriceCoefficient = table.Column<double>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_City",
                        column: x => x.IdCity,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CardID = table.Column<string>(maxLength: 12, nullable: true),
                    img = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acount_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceKm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Km = table.Column<int>(nullable: true),
                    Price = table.Column<double>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    IdVehicleType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceKm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceKm_VehicleType",
                        column: x => x.IdVehicleType,
                        principalTable: "VehicleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceTime",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeWait = table.Column<int>(nullable: true),
                    Price = table.Column<double>(nullable: true),
                    IdVehicleType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceTime_VehicleType",
                        column: x => x.IdVehicleType,
                        principalTable: "VehicleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGroup = table.Column<int>(nullable: true),
                    IdCustomer = table.Column<int>(nullable: true),
                    NameCustomer = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneCustomer = table.Column<string>(maxLength: 15, nullable: true),
                    IdVehicleType = table.Column<int>(nullable: true),
                    StartAt = table.Column<DateTime>(type: "date", nullable: true),
                    TimeWait = table.Column<int>(nullable: true),
                    PriceBooking = table.Column<double>(nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(nullable: true),
                    Mode = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Booking_Customer",
                        column: x => x.IdCustomer,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_Group",
                        column: x => x.IdGroup,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trip_VehicleType",
                        column: x => x.IdVehicleType,
                        principalTable: "VehicleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Url = table.Column<string>(maxLength: 100, nullable: true),
                    IdChannelType = table.Column<int>(nullable: true),
                    IdGroup = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channel_ChannelType",
                        column: x => x.IdChannelType,
                        principalTable: "ChannelType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Channel_Group",
                        column: x => x.IdGroup,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Content = table.Column<string>(maxLength: 200, nullable: true),
                    IdGroup = table.Column<int>(nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Group",
                        column: x => x.IdGroup,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false, defaultValueSql: "((0))"),
                    Phone = table.Column<string>(maxLength: 12, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    CardID = table.Column<string>(maxLength: 12, nullable: true),
                    img = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateByID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Driver_Manager",
                        column: x => x.CreateByID,
                        principalTable: "Manager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LatLng = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 150, nullable: true),
                    PointTypeValue = table.Column<int>(nullable: true),
                    OrderNumber = table.Column<int>(nullable: true),
                    IdTrip = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Point_Trip",
                        column: x => x.IdTrip,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Booking_Driver",
                columns: table => new
                {
                    IdTrip = table.Column<int>(nullable: false),
                    IdDriver = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    Note = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking_Driver", x => new { x.IdTrip, x.IdDriver });
                    table.ForeignKey(
                        name: "FK_Booking_Driver_Driver",
                        column: x => x.IdDriver,
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_Driver_Booking",
                        column: x => x.IdTrip,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    TokenId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdManager = table.Column<int>(nullable: true),
                    token = table.Column<string>(maxLength: 250, nullable: true),
                    expiry_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdDriver = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    LicensePlate = table.Column<string>(maxLength: 50, nullable: true),
                    IdVehicleType = table.Column<int>(nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    IdDriver = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicle_Driver",
                        column: x => x.IdDriver,
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicle_VehicleType",
                        column: x => x.IdVehicleType,
                        principalTable: "VehicleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_IdCustomer",
                table: "Booking",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_IdGroup",
                table: "Booking",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_IdVehicleType",
                table: "Booking",
                column: "IdVehicleType");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Driver_IdDriver",
                table: "Booking_Driver",
                column: "IdDriver");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_IdChannelType",
                table: "Channel",
                column: "IdChannelType");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_IdGroup",
                table: "Channel",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_CreateByID",
                table: "Driver",
                column: "CreateByID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_IdCity",
                table: "Group",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_Location_IdTrip",
                table: "Location",
                column: "IdTrip");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_RoleId",
                table: "Manager",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_News_IdGroup",
                table: "News",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_PriceKm_IdVehicleType",
                table: "PriceKm",
                column: "IdVehicleType");

            migrationBuilder.CreateIndex(
                name: "IX_PriceTime_IdVehicleType",
                table: "PriceTime",
                column: "IdVehicleType");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_IdDriver",
                table: "RefreshToken",
                column: "IdDriver");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_IdManager",
                table: "RefreshToken",
                column: "IdManager");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_IdDriver",
                table: "Vehicle",
                column: "IdDriver");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_IdVehicleType",
                table: "Vehicle",
                column: "IdVehicleType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking_Driver");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "PriceKm");

            migrationBuilder.DropTable(
                name: "PriceTime");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "ChannelType");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "VehicleType");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
