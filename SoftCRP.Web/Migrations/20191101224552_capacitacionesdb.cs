using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class capacitacionesdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tipoCapacitaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoCapacitaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "capacitaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cedula = table.Column<string>(maxLength: 20, nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Test = table.Column<string>(nullable: true),
                    tipoCapacitacionId = table.Column<int>(nullable: false),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_capacitaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_capacitaciones_tipoCapacitaciones_tipoCapacitacionId",
                        column: x => x.tipoCapacitacionId,
                        principalTable: "tipoCapacitaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_capacitaciones_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "archivoCapacitaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArchivoPath = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    TipoArchivo = table.Column<string>(nullable: true),
                    tamanio = table.Column<long>(nullable: false),
                    tramiteId = table.Column<int>(nullable: true),
                    userId = table.Column<string>(nullable: true),
                    CapacitacionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivoCapacitaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_archivoCapacitaciones_capacitaciones_CapacitacionId",
                        column: x => x.CapacitacionId,
                        principalTable: "capacitaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_archivoCapacitaciones_tramites_tramiteId",
                        column: x => x.tramiteId,
                        principalTable: "tramites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_archivoCapacitaciones_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_archivoCapacitaciones_CapacitacionId",
                table: "archivoCapacitaciones",
                column: "CapacitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_archivoCapacitaciones_tramiteId",
                table: "archivoCapacitaciones",
                column: "tramiteId");

            migrationBuilder.CreateIndex(
                name: "IX_archivoCapacitaciones_userId",
                table: "archivoCapacitaciones",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_capacitaciones_tipoCapacitacionId",
                table: "capacitaciones",
                column: "tipoCapacitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_capacitaciones_userId",
                table: "capacitaciones",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archivoCapacitaciones");

            migrationBuilder.DropTable(
                name: "capacitaciones");

            migrationBuilder.DropTable(
                name: "tipoCapacitaciones");
        }
    }
}
