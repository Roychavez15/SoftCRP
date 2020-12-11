using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class newfieldscoreinVehiculoGps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ahorro",
                table: "vehiculosGps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "score",
                table: "vehiculosGps",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ahorro",
                table: "vehiculosGps");

            migrationBuilder.DropColumn(
                name: "score",
                table: "vehiculosGps");
        }
    }
}
