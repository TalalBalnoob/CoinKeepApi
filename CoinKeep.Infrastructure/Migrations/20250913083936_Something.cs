using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoinKeep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Something : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1@user.com", "AQAAAAIAAYagAAAAEM4cZmJc1kpoIQqEjLhJYlMv6+yUuOfjR+LJoPl+au+YQ==", "user1" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2@user.com", "AQAAAAIAAYagAAAAEGD7ByIckeymf+kqAcPtLoqJ7Q9MmtLmDUwVJHhZsdWnY==", "user2" },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user3@user.com", "AQAAAAIAAYagAAAAEO9zkXe19S1v+T17mLRQUGqFc0R0Q==", "user3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
