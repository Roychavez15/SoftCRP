using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class newfieldTramites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "tramites",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Dia",
                table: "tramites",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "tramites");

            migrationBuilder.DropColumn(
                name: "Dia",
                table: "tramites");
        }
    }
}
