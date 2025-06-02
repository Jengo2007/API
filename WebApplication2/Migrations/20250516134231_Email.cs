using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class Email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cashiers_Users_UserId",
                table: "Cashiers");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Cashiers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cashiers_Users_UserId",
                table: "Cashiers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cashiers_Users_UserId",
                table: "Cashiers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Cashiers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Cashiers_Users_UserId",
                table: "Cashiers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
