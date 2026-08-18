using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarListasPreciosMontoMinimo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListasPreciosMontoMinimo",
                columns: table => new
                {
                    ListasPreciosMontoMinimoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListasPreciosID = table.Column<int>(type: "int", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    MontoMinimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasPreciosMontoMinimo", x => x.ListasPreciosMontoMinimoID);
                    table.ForeignKey(
                        name: "FK_ListasPreciosMontoMinimo_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ListasPreciosMontoMinimo_ListasPrecios_ListasPreciosID",
                        column: x => x.ListasPreciosID,
                        principalTable: "ListasPrecios",
                        principalColumn: "ListasPreciosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListasPreciosMontoMinimo_EmpresaID",
                table: "ListasPreciosMontoMinimo",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_ListasPreciosMontoMinimo_ListasPreciosID_EmpresaID",
                table: "ListasPreciosMontoMinimo",
                columns: new[] { "ListasPreciosID", "EmpresaID" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListasPreciosMontoMinimo");
        }
    }
}
