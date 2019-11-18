using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class fixlognov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "logNovedades",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "logNovedades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "logNovedades",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_logNovedades_UsuarioId",
                table: "logNovedades",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_logNovedades_AspNetUsers_UsuarioId",
                table: "logNovedades",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_logNovedades_AspNetUsers_UsuarioId",
                table: "logNovedades");

            migrationBuilder.DropIndex(
                name: "IX_logNovedades_UsuarioId",
                table: "logNovedades");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "logNovedades");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "logNovedades");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "logNovedades");
        }
    }
}
