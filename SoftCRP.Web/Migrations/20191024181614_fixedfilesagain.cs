using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class fixedfilesagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orden",
                table: "ArchivosAnalisis");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "ArchivosAnalisis",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "ArchivosAnalisis",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAnalisis_userId",
                table: "ArchivosAnalisis",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosAnalisis_AspNetUsers_userId",
                table: "ArchivosAnalisis",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosAnalisis_AspNetUsers_userId",
                table: "ArchivosAnalisis");

            migrationBuilder.DropIndex(
                name: "IX_ArchivosAnalisis_userId",
                table: "ArchivosAnalisis");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "ArchivosAnalisis");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "ArchivosAnalisis");

            migrationBuilder.AddColumn<int>(
                name: "Orden",
                table: "ArchivosAnalisis",
                nullable: false,
                defaultValue: 0);
        }
    }
}
