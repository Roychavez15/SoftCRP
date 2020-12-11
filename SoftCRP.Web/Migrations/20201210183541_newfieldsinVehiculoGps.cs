using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class newfieldsinVehiculoGps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "conductores",
                table: "vehiculosGps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "siniestros",
                table: "vehiculosGps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "talleres",
                table: "vehiculosGps",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "conductores",
                table: "vehiculosGps");

            migrationBuilder.DropColumn(
                name: "siniestros",
                table: "vehiculosGps");

            migrationBuilder.DropColumn(
                name: "talleres",
                table: "vehiculosGps");
        }
    }
}
