using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace melkikerapostgrescrud.Migrations
{
    /// <inheritdoc />
    public partial class AddArretsLignesAndPointgeoAddId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PointGeos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lon = table.Column<decimal>(type: "numeric", nullable: false),
                    lat = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointGeos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArretsLigne",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    route_long_name = table.Column<string>(type: "text", nullable: false),
                    stop_id = table.Column<string>(type: "text", nullable: false),
                    stop_name = table.Column<string>(type: "text", nullable: false),
                    stop_lon = table.Column<string>(type: "text", nullable: false),
                    stop_lat = table.Column<string>(type: "text", nullable: false),
                    operatorname = table.Column<string>(type: "text", nullable: false),
                    shortname = table.Column<string>(type: "text", nullable: false),
                    mode = table.Column<string>(type: "text", nullable: false),
                    pointgeoId = table.Column<int>(type: "integer", nullable: false),
                    nom_commune = table.Column<string>(type: "text", nullable: false),
                    code_insee = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArretsLigne", x => x.id);
                    table.ForeignKey(
                        name: "FK_ArretsLigne_PointGeos_pointgeoId",
                        column: x => x.pointgeoId,
                        principalTable: "PointGeos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArretsLigne_pointgeoId",
                table: "ArretsLigne",
                column: "pointgeoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArretsLigne");

            migrationBuilder.DropTable(
                name: "PointGeos");
        }
    }
}
