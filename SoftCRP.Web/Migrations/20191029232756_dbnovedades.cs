using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class dbnovedades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tipoNovedades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoNovedades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "novedades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cedula = table.Column<string>(maxLength: 20, nullable: false),
                    Placa = table.Column<string>(maxLength: 10, nullable: false),
                    Motivo = table.Column<string>(nullable: true),
                    ViaIngreso = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true),
                    tipoNovedadesId = table.Column<int>(nullable: false),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_novedades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_novedades_tipoNovedades_tipoNovedadesId",
                        column: x => x.tipoNovedadesId,
                        principalTable: "tipoNovedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_novedades_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "archivoNovedades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArchivoPath = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    TipoArchivo = table.Column<string>(nullable: true),
                    tamanio = table.Column<long>(nullable: false),
                    novedadId = table.Column<int>(nullable: true),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivoNovedades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_archivoNovedades_novedades_novedadId",
                        column: x => x.novedadId,
                        principalTable: "novedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_archivoNovedades_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_archivoNovedades_novedadId",
                table: "archivoNovedades",
                column: "novedadId");

            migrationBuilder.CreateIndex(
                name: "IX_archivoNovedades_userId",
                table: "archivoNovedades",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_novedades_tipoNovedadesId",
                table: "novedades",
                column: "tipoNovedadesId");

            migrationBuilder.CreateIndex(
                name: "IX_novedades_userId",
                table: "novedades",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archivoNovedades");

            migrationBuilder.DropTable(
                name: "novedades");

            migrationBuilder.DropTable(
                name: "tipoNovedades");
        }
    }
}
