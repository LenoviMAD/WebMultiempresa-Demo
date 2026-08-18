using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NormalizarProductosYPrecios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadMultiplo",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "CategoriasProductosID",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FamiliaProductosID",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImpuestosID",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarcasProductosID",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductosIDPadre",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "StockPropio",
                table: "Productos",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoriasProductosID",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlImagenWeb",
                table: "Productos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajeMarcup",
                table: "ListasPrecios",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Impuestos",
                columns: table => new
                {
                    ImpuestosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Porcentaje = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impuestos", x => x.ImpuestosID);
                    table.ForeignKey(
                        name: "FK_Impuestos_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RutasVenta",
                columns: table => new
                {
                    RutasVentaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RutasVenta", x => x.RutasVentaID);
                    table.ForeignKey(
                        name: "FK_RutasVenta_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductoRutas",
                columns: table => new
                {
                    ProductoRutasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductosID = table.Column<int>(type: "int", nullable: false),
                    RutasVentaID = table.Column<int>(type: "int", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoRutas", x => x.ProductoRutasID);
                    table.ForeignKey(
                        name: "FK_ProductoRutas_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductoRutas_Productos_ProductosID",
                        column: x => x.ProductosID,
                        principalTable: "Productos",
                        principalColumn: "ProductosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoRutas_RutasVenta_RutasVentaID",
                        column: x => x.RutasVentaID,
                        principalTable: "RutasVenta",
                        principalColumn: "RutasVentaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriasProductosID",
                table: "Productos",
                column: "CategoriasProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_FamiliaProductosID",
                table: "Productos",
                column: "FamiliaProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ImpuestosID",
                table: "Productos",
                column: "ImpuestosID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MarcasProductosID",
                table: "Productos",
                column: "MarcasProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProductosIDPadre",
                table: "Productos",
                column: "ProductosIDPadre");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_SubCategoriasProductosID",
                table: "Productos",
                column: "SubCategoriasProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_Impuestos_EmpresaID",
                table: "Impuestos",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoRutas_EmpresaID",
                table: "ProductoRutas",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoRutas_ProductosID_RutasVentaID",
                table: "ProductoRutas",
                columns: new[] { "ProductosID", "RutasVentaID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductoRutas_RutasVentaID",
                table: "ProductoRutas",
                column: "RutasVentaID");

            migrationBuilder.CreateIndex(
                name: "IX_RutasVenta_EmpresaID",
                table: "RutasVenta",
                column: "EmpresaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_CategoriaProductos_CategoriasProductosID",
                table: "Productos",
                column: "CategoriasProductosID",
                principalTable: "CategoriaProductos",
                principalColumn: "CategoriasProductosID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_FamiliaProductos_FamiliaProductosID",
                table: "Productos",
                column: "FamiliaProductosID",
                principalTable: "FamiliaProductos",
                principalColumn: "FamiliaProductosID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Impuestos_ImpuestosID",
                table: "Productos",
                column: "ImpuestosID",
                principalTable: "Impuestos",
                principalColumn: "ImpuestosID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_MarcasProductos_MarcasProductosID",
                table: "Productos",
                column: "MarcasProductosID",
                principalTable: "MarcasProductos",
                principalColumn: "MarcasProductosID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Productos_ProductosIDPadre",
                table: "Productos",
                column: "ProductosIDPadre",
                principalTable: "Productos",
                principalColumn: "ProductosID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_SubCategoriaProductos_SubCategoriasProductosID",
                table: "Productos",
                column: "SubCategoriasProductosID",
                principalTable: "SubCategoriaProductos",
                principalColumn: "SubCategoriasProductosID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_CategoriaProductos_CategoriasProductosID",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_FamiliaProductos_FamiliaProductosID",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Impuestos_ImpuestosID",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_MarcasProductos_MarcasProductosID",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Productos_ProductosIDPadre",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_SubCategoriaProductos_SubCategoriasProductosID",
                table: "Productos");

            migrationBuilder.DropTable(
                name: "Impuestos");

            migrationBuilder.DropTable(
                name: "ProductoRutas");

            migrationBuilder.DropTable(
                name: "RutasVenta");

            migrationBuilder.DropIndex(
                name: "IX_Productos_CategoriasProductosID",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_FamiliaProductosID",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_ImpuestosID",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_MarcasProductosID",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_ProductosIDPadre",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_SubCategoriasProductosID",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "CantidadMultiplo",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "CategoriasProductosID",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "FamiliaProductosID",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "ImpuestosID",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "MarcasProductosID",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "ProductosIDPadre",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "StockPropio",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "SubCategoriasProductosID",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "UrlImagenWeb",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "PorcentajeMarcup",
                table: "ListasPrecios");
        }
    }
}
