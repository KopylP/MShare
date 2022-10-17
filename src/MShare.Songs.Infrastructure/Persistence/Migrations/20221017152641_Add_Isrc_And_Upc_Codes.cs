using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MShare.Songs.Infrastructure.Persistence.Migrations
{
    public partial class Add_Isrc_And_Upc_Codes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM song");
            migrationBuilder.Sql("DELETE FROM album");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "song",
                newName: "region");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "album",
                newName: "region");

            migrationBuilder.AddColumn<string>(
                name: "isrc",
                table: "song",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "upc",
                table: "album",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isrc",
                table: "song");

            migrationBuilder.DropColumn(
                name: "upc",
                table: "album");

            migrationBuilder.RenameColumn(
                name: "region",
                table: "song",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "region",
                table: "album",
                newName: "country");
        }
    }
}
