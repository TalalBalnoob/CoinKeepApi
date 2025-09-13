using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinKeep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixHashAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEBWS00dIwzm6aum6zfyyxPsavjBxHL4bH+WALNgFmTtJTu6hlIINddyY2+bJN2n6pg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAED/hSj4vRPNwMlSSpSqSdf5JsMYUe4hNXVgSjIsq8KHfvnB8lUUYs9Ujei9bKSdIkQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEE2Vx1XhcxppJObpFP2V+sm3WzE8LiX3BhF78UyKFwiK20xTn2n7jx2U9T8hnZFr+Q==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
