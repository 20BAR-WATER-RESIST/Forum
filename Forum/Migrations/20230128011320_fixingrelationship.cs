using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class fixingrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Categories_CategoryID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CategoryID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Comments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Comments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CategoryID",
                table: "Comments",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Categories_CategoryID",
                table: "Comments",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID");
        }
    }
}
