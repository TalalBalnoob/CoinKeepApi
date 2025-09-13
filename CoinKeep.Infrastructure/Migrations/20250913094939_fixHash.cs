using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinKeep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEHa1P2y0qK8+9pGZP1v8QG1sBwtqKn5cRkzJdxhEG1cK/fG0kDnmM+Z6vMStjPpTg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEC2JUzI2tM0D+RYTK6v3LDEZtjC9V5oH2C+g+vKww/0qktYm0p1u8OJtNn1yYfK2Vw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEDHnYx0NfKxDEcJ9y+3BlV0rZZ9i8zGvFjk8rEjO6s1tqUu5+6bZQFgD0xFsZbVQfA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEM4cZmJc1kpoIQqEjLhJYlMv6+yUuOfjR+LJoPl+au+YQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEGD7ByIckeymf+kqAcPtLoqJ7Q9MmtLmDUwVJHhZsdWnY==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEO9zkXe19S1v+T17mLRQUGqFc0R0Q==");
        }
    }
}
