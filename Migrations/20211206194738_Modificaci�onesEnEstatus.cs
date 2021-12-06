using Microsoft.EntityFrameworkCore.Migrations;

namespace Subastas.Migrations
{
    public partial class ModificacionesEnEstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Estatus",
                table: "Subasta",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<float>(
                name: "Calificacion",
                table: "Subasta",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "Calificacion",
                table: "Propuesta",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Estatus",
                table: "Subasta",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "Calificacion",
                table: "Subasta",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Calificacion",
                table: "Propuesta",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);
        }
    }
}
