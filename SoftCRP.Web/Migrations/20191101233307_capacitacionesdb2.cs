using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class capacitacionesdb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "tipoCapacitaciones",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "tipoCapacitaciones",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
