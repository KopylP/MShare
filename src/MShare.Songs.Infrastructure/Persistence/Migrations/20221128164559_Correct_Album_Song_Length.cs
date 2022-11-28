using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MShare.Songs.Infrastructure.Persistence.Migrations
{
    public partial class Correct_Album_Song_Length : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "source_url",
                table: "song",
                type: "varchar(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "source_url",
                table: "album",
                type: "varchar(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "source_url",
                table: "song",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "source_url",
                table: "album",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2000)");
        }
    }
}
