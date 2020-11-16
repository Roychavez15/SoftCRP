using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class newTablesV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<string>(nullable: true),
                    Placa = table.Column<string>(nullable: true),
                    gps_id = table.Column<string>(nullable: true),
                    gps_provider = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vehiculos_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vehiculosGps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    vehiculoId = table.Column<int>(nullable: true),
                    dia = table.Column<int>(nullable: false),
                    mes = table.Column<int>(nullable: false),
                    anio = table.Column<int>(nullable: false),
                    trips = table.Column<int>(nullable: false),
                    kilometerstraveled = table.Column<int>(nullable: false),
                    speeding = table.Column<int>(nullable: false),
                    hardbraking = table.Column<int>(nullable: false),
                    sharpacceleration = table.Column<int>(nullable: false),
                    sharpturn = table.Column<int>(nullable: false),
                    latitude = table.Column<string>(nullable: true),
                    longitude = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehiculosGps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vehiculosGps_vehiculos_vehiculoId",
                        column: x => x.vehiculoId,
                        principalTable: "vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vehiculos_userId",
                table: "vehiculos",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_vehiculosGps_vehiculoId",
                table: "vehiculosGps",
                column: "vehiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vehiculosGps");

            migrationBuilder.DropTable(
                name: "vehiculos");
        }
    }
}
