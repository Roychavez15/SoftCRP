using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class newtableaccesos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accesos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    Informacion = table.Column<bool>(nullable: false),
                    Analisis = table.Column<bool>(nullable: false),
                    Seguimiento = table.Column<bool>(nullable: false),
                    Tramites = table.Column<bool>(nullable: false),
                    Capacitaciones = table.Column<bool>(nullable: false),
                    Conduccion = table.Column<bool>(nullable: false),
                    Mantenimiento = table.Column<bool>(nullable: false),
                    Siniestros = table.Column<bool>(nullable: false),
                    Sustitutos = table.Column<bool>(nullable: false),
                    Graficos = table.Column<bool>(nullable: false),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accesos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_accesos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accesos_UserId",
                table: "accesos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accesos");
        }
    }
}
