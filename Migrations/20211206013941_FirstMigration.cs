using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Subastas.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    RolID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreRol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.RolID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RolID = table.Column<int>(nullable: false),
                    NombreUsuario = table.Column<string>(nullable: true),
                    RFC = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Calificacion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_RolID",
                        column: x => x.RolID,
                        principalTable: "Rol",
                        principalColumn: "RolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Propuesta",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SubastaID = table.Column<int>(nullable: false),
                    UsuarioID = table.Column<int>(nullable: false),
                    TituloPropuesta = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Estatus = table.Column<int>(nullable: false),
                    Calificacion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propuesta", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Propuesta_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subasta",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UsuarioID = table.Column<int>(nullable: false),
                    NombreProyecto = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Calificacion = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subasta", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Subasta_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Propuesta_UsuarioID",
                table: "Propuesta",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Subasta_UsuarioID",
                table: "Subasta",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RolID",
                table: "Usuario",
                column: "RolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Propuesta");

            migrationBuilder.DropTable(
                name: "Subasta");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
