using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class DBTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UsersTypes_UserTypeID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersTypes",
                table: "UsersTypes");

            migrationBuilder.RenameTable(
                name: "UsersTypes",
                newName: "UserTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTypes",
                table: "UserTypes",
                column: "UserTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_UserTypeID",
                table: "Users",
                column: "UserTypeID",
                principalTable: "UserTypes",
                principalColumn: "UserTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_UserTypeID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTypes",
                table: "UserTypes");

            migrationBuilder.RenameTable(
                name: "UserTypes",
                newName: "UsersTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersTypes",
                table: "UsersTypes",
                column: "UserTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UsersTypes_UserTypeID",
                table: "Users",
                column: "UserTypeID",
                principalTable: "UsersTypes",
                principalColumn: "UserTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
