using Microsoft.EntityFrameworkCore.Migrations;

namespace Git.Migrations
{
    public partial class RefactorinFluenApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_Users_CreatorId",
                table: "Commits");

            migrationBuilder.DropForeignKey(
                name: "FK_Repositories_Users_OwnerId",
                table: "Repositories");

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_Users_CreatorId",
                table: "Commits",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repositories_Users_OwnerId",
                table: "Repositories",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_Users_CreatorId",
                table: "Commits");

            migrationBuilder.DropForeignKey(
                name: "FK_Repositories_Users_OwnerId",
                table: "Repositories");

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_Users_CreatorId",
                table: "Commits",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Repositories_Users_OwnerId",
                table: "Repositories",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
