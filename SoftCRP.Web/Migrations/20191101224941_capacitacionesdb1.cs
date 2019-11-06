using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class capacitacionesdb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cedula",
                table: "capacitaciones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cedula",
                table: "capacitaciones",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
