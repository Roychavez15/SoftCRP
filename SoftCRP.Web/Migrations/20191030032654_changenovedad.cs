using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class changenovedad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSolucion",
                table: "novedades",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Solucion",
                table: "novedades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userSolucionId",
                table: "novedades",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_novedades_userSolucionId",
                table: "novedades",
                column: "userSolucionId");

            migrationBuilder.AddForeignKey(
                name: "FK_novedades_AspNetUsers_userSolucionId",
                table: "novedades",
                column: "userSolucionId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_novedades_AspNetUsers_userSolucionId",
                table: "novedades");

            migrationBuilder.DropIndex(
                name: "IX_novedades_userSolucionId",
                table: "novedades");

            migrationBuilder.DropColumn(
                name: "FechaSolucion",
                table: "novedades");

            migrationBuilder.DropColumn(
                name: "Solucion",
                table: "novedades");

            migrationBuilder.DropColumn(
                name: "userSolucionId",
                table: "novedades");
        }
    }
}
