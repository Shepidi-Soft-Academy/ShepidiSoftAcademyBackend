using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShepidiSoft.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OrganizationPositionREf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "OrganizationPositions",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationPositions_Name",
                table: "OrganizationPositions",
                newName: "IX_OrganizationPositions_Title");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationPositions_IsActive_Name",
                table: "OrganizationPositions",
                newName: "IX_OrganizationPositions_IsActive_Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "OrganizationPositions",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationPositions_Title",
                table: "OrganizationPositions",
                newName: "IX_OrganizationPositions_Name");

            migrationBuilder.RenameIndex(
                name: "IX_OrganizationPositions_IsActive_Title",
                table: "OrganizationPositions",
                newName: "IX_OrganizationPositions_IsActive_Name");
        }
    }
}
