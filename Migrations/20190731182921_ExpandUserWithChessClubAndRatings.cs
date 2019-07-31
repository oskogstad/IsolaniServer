using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Isolani.Migrations
{
    public partial class ExpandUserWithChessClubAndRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BirthYear",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BlitzRating",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChessClubId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RapidRating",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StandardRating",
                table: "Users",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ChessClubs_ChessClubId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChessClubId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BlitzRating",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChessClubId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RapidRating",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StandardRating",
                table: "Users");
        }
    }
}
