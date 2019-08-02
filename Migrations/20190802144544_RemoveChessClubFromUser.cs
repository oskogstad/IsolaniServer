using Microsoft.EntityFrameworkCore.Migrations;

namespace Isolani.Migrations
{
    public partial class RemoveChessClubFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ChessClubs_ChessClubId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChessClubId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_ChessClubId",
                table: "Users",
                column: "ChessClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ChessClubs_ChessClubId",
                table: "Users",
                column: "ChessClubId",
                principalTable: "ChessClubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
