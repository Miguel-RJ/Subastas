using Microsoft.EntityFrameworkCore.Migrations;

namespace Subastas.Migrations
{
    public partial class ModificarTipoCalificaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Calificacion",
                table: "Subasta",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Calificacion",
                table: "Propuesta",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Calificacion",
                table: "Subasta",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "Calificacion",
                table: "Propuesta",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
