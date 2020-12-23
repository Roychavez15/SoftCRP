using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class newfieldtramitesalarma : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Desde",
                table: "tramites",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "tramites",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Hasta",
                table: "tramites",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desde",
                table: "tramites");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "tramites");

            migrationBuilder.DropColumn(
                name: "Hasta",
                table: "tramites");
        }
    }
}
