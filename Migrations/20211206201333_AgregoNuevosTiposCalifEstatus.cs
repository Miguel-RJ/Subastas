using Microsoft.EntityFrameworkCore.Migrations;

namespace Subastas.Migrations
{
    public partial class AgregoNuevosTiposCalifEstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Grade",
                table: "Subasta",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Subasta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Grade",
                table: "Propuesta",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Propuesta",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Subasta");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Subasta");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Propuesta");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Propuesta");
        }
    }
}
