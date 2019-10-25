using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class newtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Placa = table.Column<string>(maxLength: 10, nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true),
                    tipoAnalisisId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Analises_TiposAnalisis_tipoAnalisisId",
                        column: x => x.tipoAnalisisId,
                        principalTable: "TiposAnalisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArchivosAnalisis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArchivoPath = table.Column<string>(nullable: true),
                    AnalisisId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosAnalisis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivosAnalisis_Analises_AnalisisId",
                        column: x => x.AnalisisId,
                        principalTable: "Analises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analises_tipoAnalisisId",
                table: "Analises",
                column: "tipoAnalisisId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAnalisis_AnalisisId",
                table: "ArchivosAnalisis",
                column: "AnalisisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivosAnalisis");

            migrationBuilder.DropTable(
                name: "Analises");
        }
    }
}
