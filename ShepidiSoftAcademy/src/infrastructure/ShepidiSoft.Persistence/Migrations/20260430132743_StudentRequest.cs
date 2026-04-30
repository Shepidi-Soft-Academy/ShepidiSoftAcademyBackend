using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShepidiSoft.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StudentRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "StudentRequests",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "StudentRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentRequests_StudentId1",
                table: "StudentRequests",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRequests_Students_StudentId1",
                table: "StudentRequests",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentRequests_Students_StudentId1",
                table: "StudentRequests");

            migrationBuilder.DropIndex(
                name: "IX_StudentRequests_StudentId1",
                table: "StudentRequests");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "StudentRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "StudentRequests",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3000)",
                oldMaxLength: 3000);
        }
    }
}
