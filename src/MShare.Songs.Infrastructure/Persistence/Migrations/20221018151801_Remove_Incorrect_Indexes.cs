using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MShare.Songs.Infrastructure.Persistence.Migrations
{
    public partial class Remove_Incorrect_Indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_Isrc_Unique",
                table: "song");

            migrationBuilder.DropIndex(
                name: "IDX_Upc_Unique",
                table: "album");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IDX_Isrc_Unique",
                table: "song",
                column: "isrc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IDX_Upc_Unique",
                table: "album",
                column: "upc",
                unique: true);
        }
    }
}
