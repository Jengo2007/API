using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersAndCashiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { new Guid("19170bf0-83c0-4e86-b566-1c50b92a5da0"), "cashier@mail.com", "hashed_password_cashier", 1, "cashier1" },
                    { new Guid("217213b5-9e20-43cb-a3ed-97085ed501e8"), "admin@mail.com", "hashed_password_admin", 0, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Cashiers",
                columns: new[] { "CashierID", "CashierName", "CashierPhoneNumber", "UserId" },
                values: new object[] { new Guid("33d21ae6-0a1a-4e81-a2b2-42c57fcd4e59"), "Иван Иванов", 900123456, new Guid("19170bf0-83c0-4e86-b566-1c50b92a5da0") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cashiers",
                keyColumn: "CashierID",
                keyValue: new Guid("33d21ae6-0a1a-4e81-a2b2-42c57fcd4e59"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("217213b5-9e20-43cb-a3ed-97085ed501e8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("19170bf0-83c0-4e86-b566-1c50b92a5da0"));
        }
    }
}
