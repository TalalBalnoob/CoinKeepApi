using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoinKeep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    User_id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Users_User_id",
                        column: x => x.User_id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Type", "UserId", "User_id" },
                values: new object[,]
                {
                    { 1, "Salary", 0, null, null },
                    { 2, "Freelance", 0, null, null },
                    { 3, "Interest", 0, null, null },
                    { 4, "Groceries", 1, null, null },
                    { 5, "Rent", 1, null, null },
                    { 6, "Utilities", 1, null, null },
                    { 7, "Entertainment", 1, null, null },
                    { 8, "Transport", 1, null, null },
                    { 9, "Investment", 0, null, null },
                    { 10, "Health", 1, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_User_id",
                table: "Categories",
                column: "User_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
