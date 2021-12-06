using Microsoft.EntityFrameworkCore.Migrations;

namespace Subastas.Migrations
{
    public partial class ChangeTypePart1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calificacion",
                table: "Subasta");

            migrationBuilder.DropColumn(
                name: "Calificacion",
                table: "Propuesta");

            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "Propuesta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Calificacion",
                table: "Subasta",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Calificacion",
                table: "Propuesta",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estatus",
                table: "Propuesta",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
