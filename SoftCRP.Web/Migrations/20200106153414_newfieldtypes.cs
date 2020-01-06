using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class newfieldtypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "tipoTramites",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "TiposAnalisis",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "tipoNovedades",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "tipoCapacitaciones",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "tipoTramites");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "TiposAnalisis");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "tipoNovedades");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "tipoCapacitaciones");
        }
    }
}
