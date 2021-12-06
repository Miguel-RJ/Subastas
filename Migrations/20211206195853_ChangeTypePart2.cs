using Microsoft.EntityFrameworkCore.Migrations;

namespace Subastas.Migrations
{
    public partial class ChangeTypePart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Calificacion",
                table: "Subasta",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Calificacion",
                table: "Propuesta",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estatus",
                table: "Propuesta",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
