using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarArrastreYHistorial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CantidadXPDV",
                table: "Combos",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoArrastre",
                table: "Combos",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "TomaProdParaArrastre",
                table: "Combos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ComboLogs",
                columns: table => new
                {
                    ComboLogsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CombosID = table.Column<int>(type: "int", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    UsuariosID = table.Column<int>(type: "int", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboLogs", x => x.ComboLogsID);
                    table.ForeignKey(
                        name: "FK_ComboLogs_Combos_CombosID",
                        column: x => x.CombosID,
                        principalTable: "Combos",
                        principalColumn: "CombosID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboMarcas",
                columns: table => new
                {
                    ComboMarcasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CombosID = table.Column<int>(type: "int", nullable: false),
                    MarcasID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboMarcas", x => x.ComboMarcasID);
                    table.ForeignKey(
                        name: "FK_ComboMarcas_Combos_CombosID",
                        column: x => x.CombosID,
                        principalTable: "Combos",
                        principalColumn: "CombosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboMarcas_Marcas_MarcasID",
                        column: x => x.MarcasID,
                        principalTable: "Marcas",
                        principalColumn: "MarcasID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComboRubros",
                columns: table => new
                {
                    ComboRubrosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CombosID = table.Column<int>(type: "int", nullable: false),
                    RubrosID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboRubros", x => x.ComboRubrosID);
                    table.ForeignKey(
                        name: "FK_ComboRubros_Combos_CombosID",
                        column: x => x.CombosID,
                        principalTable: "Combos",
                        principalColumn: "CombosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboRubros_Rubros_RubrosID",
                        column: x => x.RubrosID,
                        principalTable: "Rubros",
                        principalColumn: "RubrosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboLogs_CombosID",
                table: "ComboLogs",
                column: "CombosID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboMarcas_CombosID_MarcasID",
                table: "ComboMarcas",
                columns: new[] { "CombosID", "MarcasID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComboMarcas_MarcasID",
                table: "ComboMarcas",
                column: "MarcasID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboRubros_CombosID_RubrosID",
                table: "ComboRubros",
                columns: new[] { "CombosID", "RubrosID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComboRubros_RubrosID",
                table: "ComboRubros",
                column: "RubrosID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComboLogs");

            migrationBuilder.DropTable(
                name: "ComboMarcas");

            migrationBuilder.DropTable(
                name: "ComboRubros");

            migrationBuilder.DropColumn(
                name: "CantidadXPDV",
                table: "Combos");

            migrationBuilder.DropColumn(
                name: "MontoArrastre",
                table: "Combos");

            migrationBuilder.DropColumn(
                name: "TomaProdParaArrastre",
                table: "Combos");
        }
    }
}
