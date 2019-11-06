using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class tramitesdb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tipoTramiteId",
                table: "tramites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tramites_tipoTramiteId",
                table: "tramites",
                column: "tipoTramiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_tramites_tipoTramites_tipoTramiteId",
                table: "tramites",
                column: "tipoTramiteId",
                principalTable: "tipoTramites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tramites_tipoTramites_tipoTramiteId",
                table: "tramites");

            migrationBuilder.DropIndex(
                name: "IX_tramites_tipoTramiteId",
                table: "tramites");

            migrationBuilder.DropColumn(
                name: "tipoTramiteId",
                table: "tramites");
        }
    }
}
