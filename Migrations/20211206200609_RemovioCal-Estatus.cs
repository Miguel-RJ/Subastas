using Microsoft.EntityFrameworkCore.Migrations;

namespace Subastas.Migrations
{
    public partial class RemovioCalEstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calificacion",
                table: "Subasta");

            migrationBuilder.DropColumn(
                name: "Estatus",
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

            migrationBuilder.AddColumn<int>(
                name: "Estatus",
                table: "Subasta",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
