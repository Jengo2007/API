using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class ADDCASHIERUSER : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Cashiers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cashiers_UserId",
                table: "Cashiers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cashiers_Users_UserId",
                table: "Cashiers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cashiers_Users_UserId",
                table: "Cashiers");

            migrationBuilder.DropIndex(
                name: "IX_Cashiers_UserId",
                table: "Cashiers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cashiers");
        }
    }
}
