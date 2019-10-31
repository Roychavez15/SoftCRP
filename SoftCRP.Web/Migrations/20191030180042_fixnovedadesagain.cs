using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class fixnovedadesagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_novedades_tipoNovedades_tipoNovedadesId",
                table: "novedades");

            migrationBuilder.DropIndex(
                name: "IX_novedades_tipoNovedadesId",
                table: "novedades");

            migrationBuilder.DropColumn(
                name: "tipoNovedadesId",
                table: "novedades");

            migrationBuilder.AlterColumn<string>(
                name: "ViaIngreso",
                table: "novedades",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Motivo",
                table: "novedades",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaSolucion",
                table: "novedades",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "SubMotivo",
                table: "novedades",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubMotivo",
                table: "novedades");

            migrationBuilder.AlterColumn<string>(
                name: "ViaIngreso",
                table: "novedades",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Motivo",
                table: "novedades",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaSolucion",
                table: "novedades",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tipoNovedadesId",
                table: "novedades",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_novedades_tipoNovedadesId",
                table: "novedades",
                column: "tipoNovedadesId");

            migrationBuilder.AddForeignKey(
                name: "FK_novedades_tipoNovedades_tipoNovedadesId",
                table: "novedades",
                column: "tipoNovedadesId",
                principalTable: "tipoNovedades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
