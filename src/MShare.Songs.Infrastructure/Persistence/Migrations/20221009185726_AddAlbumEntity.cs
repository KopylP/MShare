﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MShare.Songs.Infrastructure.Persistence.Migrations
{
    public partial class AddAlbumEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "album",
                columns: table => new
                {
                    service_type = table.Column<string>(type: "varchar(100)", nullable: false),
                    source_id = table.Column<string>(type: "varchar(100)", nullable: false),
                    name = table.Column<string>(type: "varchar(500)", nullable: false),
                    artist_name = table.Column<string>(type: "varchar(1000)", nullable: false),
                    image_url = table.Column<string>(type: "varchar(300)", nullable: false),
                    source_url = table.Column<string>(type: "varchar(200)", nullable: false),
                    country = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => new { x.source_id, x.service_type });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "album");
        }
    }
}
