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
                name: "PropertyItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConstructionYear = table.Column<int>(type: "int", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    OwnerVAT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyOwnerIds = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VAT = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "PropertyRepairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledRepair = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepairType = table.Column<int>(type: "int", nullable: false),
                    RepairDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepairAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyItemId = table.Column<int>(type: "int", nullable: false),
                    RepairStatus = table.Column<int>(type: "int", nullable: false),
                    RepairPrice = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyRepairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyRepairs_PropertyItems_PropertyItemId",
                        column: x => x.PropertyItemId,
                        principalTable: "PropertyItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyItemPropertyOwner",
                columns: table => new
                {
                    OwnersId = table.Column<int>(type: "int", nullable: false),
                    PropertiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyItemPropertyOwner", x => new { x.OwnersId, x.PropertiesId });
                    table.ForeignKey(
                        name: "FK_PropertyItemPropertyOwner_PropertyItems_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "PropertyItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyItemPropertyOwner_PropertyOwners_OwnersId",
                        column: x => x.OwnersId,
                        principalTable: "PropertyOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyItemPropertyOwner_PropertiesId",
                table: "PropertyItemPropertyOwner",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwners_VAT",
                table: "PropertyOwners",
                column: "VAT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRepairs_PropertyItemId",
                table: "PropertyRepairs",
                column: "PropertyItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyItemPropertyOwner");

            migrationBuilder.DropTable(
                name: "PropertyRepairs");

            migrationBuilder.DropTable(
                name: "PropertyOwners");

            migrationBuilder.DropTable(
                name: "PropertyItems");
        }
    }
}
