using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class errortablecap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_archivoCapacitaciones_capacitaciones_CapacitacionId",
                table: "archivoCapacitaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_archivoCapacitaciones_tramites_tramiteId",
                table: "archivoCapacitaciones");

            migrationBuilder.DropIndex(
                name: "IX_archivoCapacitaciones_tramiteId",
                table: "archivoCapacitaciones");

            migrationBuilder.DropColumn(
                name: "tramiteId",
                table: "archivoCapacitaciones");

            migrationBuilder.RenameColumn(
                name: "CapacitacionId",
                table: "archivoCapacitaciones",
                newName: "capacitacionId");

            migrationBuilder.RenameIndex(
                name: "IX_archivoCapacitaciones_CapacitacionId",
                table: "archivoCapacitaciones",
                newName: "IX_archivoCapacitaciones_capacitacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_archivoCapacitaciones_capacitaciones_capacitacionId",
                table: "archivoCapacitaciones",
                column: "capacitacionId",
                principalTable: "capacitaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_archivoCapacitaciones_capacitaciones_capacitacionId",
                table: "archivoCapacitaciones");

            migrationBuilder.RenameColumn(
                name: "capacitacionId",
                table: "archivoCapacitaciones",
                newName: "CapacitacionId");

            migrationBuilder.RenameIndex(
                name: "IX_archivoCapacitaciones_capacitacionId",
                table: "archivoCapacitaciones",
                newName: "IX_archivoCapacitaciones_CapacitacionId");

            migrationBuilder.AddColumn<int>(
                name: "tramiteId",
                table: "archivoCapacitaciones",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_archivoCapacitaciones_tramiteId",
                table: "archivoCapacitaciones",
                column: "tramiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_archivoCapacitaciones_capacitaciones_CapacitacionId",
                table: "archivoCapacitaciones",
                column: "CapacitacionId",
                principalTable: "capacitaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_archivoCapacitaciones_tramites_tramiteId",
                table: "archivoCapacitaciones",
                column: "tramiteId",
                principalTable: "tramites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
