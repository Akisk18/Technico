using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VAT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyAdrress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConstructionYear = table.Column<int>(type: "int", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyItems_PropertyOwners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "PropertyOwners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PropertyRepairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledRepair = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepairType = table.Column<int>(type: "int", nullable: false),
                    RepairDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepairAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepairStatus = table.Column<int>(type: "int", nullable: false),
                    RepairPrice = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyRepairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyRepairs_PropertyItems_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "PropertyItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyItems_OwnerId",
                table: "PropertyItems",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRepairs_PropertyId",
                table: "PropertyRepairs",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyRepairs");

            migrationBuilder.DropTable(
                name: "PropertyItems");

            migrationBuilder.DropTable(
                name: "PropertyOwners");
        }
    }
}
