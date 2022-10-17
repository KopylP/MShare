using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MShare.Songs.Infrastructure.Persistence.Migrations
{
    public partial class Update_Album_And_Song_Add_Indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM song");
            migrationBuilder.Sql("DELETE FROM album");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_Isrc_Unique",
                table: "song");

            migrationBuilder.DropIndex(
                name: "IDX_Upc_Unique",
                table: "album");
        }
    }
}
