using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MShare.Songs.Infrastructure.Persistence.Migrations
{
    public partial class Correct_Upc_Isrc_Indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX UN_Song_Region_Isrc");
            migrationBuilder.Sql("DROP INDEX UN_Album_Region_Upc");
            migrationBuilder.Sql("CREATE UNIQUE INDEX UN_Album_Upc_ServiceType_Region ON album(upc, service_type, region)");
            migrationBuilder.Sql("CREATE UNIQUE INDEX UN_Song_Isrc_ServiceType_Region ON song(Isrc, service_type, region)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX UN_Album_Upc_ServiceType_Region");
            migrationBuilder.Sql("DROP INDEX UN_Song_Isrc_ServiceType_Region");
            migrationBuilder.Sql("CREATE UNIQUE INDEX UN_Song_Region_Isrc ON song(region, isrc)");
            migrationBuilder.Sql("CREATE UNIQUE INDEX UN_Album_Region_Upc ON album(region, upc)");
        }
    }
}