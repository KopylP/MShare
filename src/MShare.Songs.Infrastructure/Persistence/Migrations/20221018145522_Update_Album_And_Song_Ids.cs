using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MShare.Songs.Infrastructure.Persistence.Migrations
{
    public partial class Update_Album_And_Song_Ids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Song",
                table: "song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "album");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "song",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "album",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Song",
                table: "song",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "album",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Song",
                table: "song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "album");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "song");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "album");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Song",
                table: "song",
                columns: new[] { "source_id", "service_type" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "album",
                columns: new[] { "source_id", "service_type" });
        }
    }
}
