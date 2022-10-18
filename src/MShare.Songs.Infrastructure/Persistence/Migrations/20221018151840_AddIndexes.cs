using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MShare.Songs.Infrastructure.Persistence.Migrations
{
    public partial class AddIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE UNIQUE INDEX UN_Song_Region_Isrc ON song(region, isrc)");
            migrationBuilder.Sql("CREATE UNIQUE INDEX UN_Album_Region_Upc ON album(region, upc)");
            migrationBuilder.Sql("CREATE UNIQUE INDEX UN_Song_Region_ServiceType_SourceId ON song(region, service_type, source_id)");
            migrationBuilder.Sql("CREATE UNIQUE INDEX UN_Album_Region_ServiceType_SourceId ON album(region, service_type, source_id)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX UN_Song_Region_Isrc");
            migrationBuilder.Sql("DROP INDEX UN_Album_Region_Upc");
            migrationBuilder.Sql("DROP INDEX UN_Song_Region_ServiceType_SourceId");
            migrationBuilder.Sql("DROP INDEX UN_Album_Region_ServiceType_SourceId");
        }
    }
}
