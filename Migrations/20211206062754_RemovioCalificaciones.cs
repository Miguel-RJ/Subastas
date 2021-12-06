using Microsoft.EntityFrameworkCore.Migrations;

namespace Subastas.Migrations
{
    public partial class RemovioCalificaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calificacion",
                table: "Usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Calificacion",
                table: "Usuario",
                type: "int",
                nullable: true);
        }
    }
}
