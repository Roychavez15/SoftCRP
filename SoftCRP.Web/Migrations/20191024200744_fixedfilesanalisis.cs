using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftCRP.Web.Migrations
{
    public partial class fixedfilesanalisis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosAnalisis_Analises_AnalisisId",
                table: "ArchivosAnalisis");

            migrationBuilder.RenameColumn(
                name: "AnalisisId",
                table: "ArchivosAnalisis",
                newName: "analisisId");

            migrationBuilder.RenameIndex(
                name: "IX_ArchivosAnalisis_AnalisisId",
                table: "ArchivosAnalisis",
                newName: "IX_ArchivosAnalisis_analisisId");

            migrationBuilder.AlterColumn<int>(
                name: "analisisId",
                table: "ArchivosAnalisis",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Analises",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Analises_userId",
                table: "Analises",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analises_AspNetUsers_userId",
                table: "Analises",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosAnalisis_Analises_analisisId",
                table: "ArchivosAnalisis",
                column: "analisisId",
                principalTable: "Analises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analises_AspNetUsers_userId",
                table: "Analises");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosAnalisis_Analises_analisisId",
                table: "ArchivosAnalisis");

            migrationBuilder.DropIndex(
                name: "IX_Analises_userId",
                table: "Analises");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Analises");

            migrationBuilder.RenameColumn(
                name: "analisisId",
                table: "ArchivosAnalisis",
                newName: "AnalisisId");

            migrationBuilder.RenameIndex(
                name: "IX_ArchivosAnalisis_analisisId",
                table: "ArchivosAnalisis",
                newName: "IX_ArchivosAnalisis_AnalisisId");

            migrationBuilder.AlterColumn<int>(
                name: "AnalisisId",
                table: "ArchivosAnalisis",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosAnalisis_Analises_AnalisisId",
                table: "ArchivosAnalisis",
                column: "AnalisisId",
                principalTable: "Analises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
