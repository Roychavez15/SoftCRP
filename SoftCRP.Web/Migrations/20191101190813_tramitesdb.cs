using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class tramitesdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tipoTramites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoTramites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tramites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cedula = table.Column<string>(maxLength: 20, nullable: false),
                    Placa = table.Column<string>(maxLength: 10, nullable: false),
                    Mes = table.Column<string>(nullable: false),
                    Anio = table.Column<string>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tramites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tramites_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "archivoTramites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArchivoPath = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    TipoArchivo = table.Column<string>(nullable: true),
                    tamanio = table.Column<long>(nullable: false),
                    tramiteId = table.Column<int>(nullable: true),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivoTramites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_archivoTramites_tramites_tramiteId",
                        column: x => x.tramiteId,
                        principalTable: "tramites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_archivoTramites_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_archivoTramites_tramiteId",
                table: "archivoTramites",
                column: "tramiteId");

            migrationBuilder.CreateIndex(
                name: "IX_archivoTramites_userId",
                table: "archivoTramites",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_tramites_userId",
                table: "tramites",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archivoTramites");

            migrationBuilder.DropTable(
                name: "tipoTramites");

            migrationBuilder.DropTable(
                name: "tramites");
        }
    }
}
