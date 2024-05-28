using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_userId",
                table: "Todos");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Todos",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Todos_userId",
                table: "Todos",
                newName: "IX_Todos_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_UserId",
                table: "Todos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UserId",
                table: "Todos");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Todos",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Todos_UserId",
                table: "Todos",
                newName: "IX_Todos_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_userId",
                table: "Todos",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
