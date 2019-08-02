using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Isolani.Migrations
{
    public partial class CreateGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlayerWhiteId = table.Column<Guid>(nullable: false),
                    PlayerBlackId = table.Column<Guid>(nullable: false),
                    StartDateUtc = table.Column<DateTime>(nullable: false),
                    TournamentId = table.Column<Guid>(nullable: false),
                    GameState = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Users_PlayerBlackId",
                        column: x => x.PlayerBlackId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Users_PlayerWhiteId",
                        column: x => x.PlayerWhiteId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerBlackId",
                table: "Games",
                column: "PlayerBlackId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerWhiteId",
                table: "Games",
                column: "PlayerWhiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
