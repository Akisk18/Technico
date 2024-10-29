using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyOwnerId",
                table: "PropertyItems");

            migrationBuilder.AddColumn<string>(
                name: "PropertyOwnerIds",
                table: "PropertyItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyOwnerIds",
                table: "PropertyItems");

            migrationBuilder.AddColumn<int>(
                name: "PropertyOwnerId",
                table: "PropertyItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
