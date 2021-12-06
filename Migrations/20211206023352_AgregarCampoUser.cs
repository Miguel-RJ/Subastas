using Microsoft.EntityFrameworkCore.Migrations;

namespace Subastas.Migrations
{
    public partial class AgregarCampoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TagUsuario",
                table: "Usuario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagUsuario",
                table: "Usuario");
        }
    }
}
