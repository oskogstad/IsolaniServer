using Microsoft.EntityFrameworkCore.Migrations;

namespace Isolani.Migrations
{
    public partial class AddUtcToUserTimestamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLoginDate",
                table: "Users",
                newName: "LastLoginDateUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Users",
                newName: "CreatedDateUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastLoginDateUtc",
                table: "Users",
                newName: "LastLoginDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDateUtc",
                table: "Users",
                newName: "CreatedDate");
        }
    }
}
