using Microsoft.EntityFrameworkCore.Migrations;

namespace Subastas.Migrations
{
    public partial class Intento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CambioDeTipo",
                table: "Subasta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CambioDeTipo",
                table: "Propuesta",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CambioDeTipo",
                table: "Subasta");

            migrationBuilder.DropColumn(
                name: "CambioDeTipo",
                table: "Propuesta");
        }
    }
}
