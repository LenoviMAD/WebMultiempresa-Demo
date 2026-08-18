using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NormalizarFamiliaProductoMarca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamiliaProductos_MarcasProductos_MarcasProductosID",
                table: "FamiliaProductos");

            migrationBuilder.DropIndex(
                name: "IX_FamiliaProductos_MarcasProductosID",
                table: "FamiliaProductos");

            migrationBuilder.DropColumn(
                name: "MarcasProductosID",
                table: "FamiliaProductos");

            migrationBuilder.CreateTable(
                name: "FamiliaProductoMarcas",
                columns: table => new
                {
                    FamiliaProductoMarcasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    FamiliaProductosID = table.Column<int>(type: "int", nullable: false),
                    MarcasProductosID = table.Column<int>(type: "int", nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamiliaProductoMarcas", x => x.FamiliaProductoMarcasID);
                    table.ForeignKey(
                        name: "FK_FamiliaProductoMarcas_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FamiliaProductoMarcas_FamiliaProductos_FamiliaProductosID",
                        column: x => x.FamiliaProductosID,
                        principalTable: "FamiliaProductos",
                        principalColumn: "FamiliaProductosID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FamiliaProductoMarcas_MarcasProductos_MarcasProductosID",
                        column: x => x.MarcasProductosID,
                        principalTable: "MarcasProductos",
                        principalColumn: "MarcasProductosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FamiliaProductoMarcas_EmpresaID",
                table: "FamiliaProductoMarcas",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_FamiliaProductoMarcas_FamiliaProductosID_MarcasProductosID",
                table: "FamiliaProductoMarcas",
                columns: new[] { "FamiliaProductosID", "MarcasProductosID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamiliaProductoMarcas_MarcasProductosID",
                table: "FamiliaProductoMarcas",
                column: "MarcasProductosID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamiliaProductoMarcas");

            migrationBuilder.AddColumn<int>(
                name: "MarcasProductosID",
                table: "FamiliaProductos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamiliaProductos_MarcasProductosID",
                table: "FamiliaProductos",
                column: "MarcasProductosID");

            migrationBuilder.AddForeignKey(
                name: "FK_FamiliaProductos_MarcasProductos_MarcasProductosID",
                table: "FamiliaProductos",
                column: "MarcasProductosID",
                principalTable: "MarcasProductos",
                principalColumn: "MarcasProductosID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
