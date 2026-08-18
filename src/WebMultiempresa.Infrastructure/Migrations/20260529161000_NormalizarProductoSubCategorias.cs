using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using WebMultiempresa.Infrastructure.Persistence;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(AppDbContext))]
    [Migration("20260529161000_NormalizarProductoSubCategorias")]
    public partial class NormalizarProductoSubCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Quitar FKs directas de Productos hacia CategoriaProductos y SubCategoriaProductos
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_CategoriaProductos_CategoriasProductosID",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_SubCategoriaProductos_SubCategoriasProductosID",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_CategoriasProductosID",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_SubCategoriasProductosID",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "CategoriasProductosID",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "SubCategoriasProductosID",
                table: "Productos");

            // Crear tabla de relación N:M
            migrationBuilder.CreateTable(
                name: "ProductoSubCategorias",
                columns: table => new
                {
                    ProductoSubCategoriasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    ProductosID = table.Column<int>(type: "int", nullable: false),
                    SubCategoriasProductosID = table.Column<int>(type: "int", nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoSubCategorias", x => x.ProductoSubCategoriasID);
                    table.ForeignKey(
                        name: "FK_ProductoSubCategorias_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductoSubCategorias_Productos_ProductosID",
                        column: x => x.ProductosID,
                        principalTable: "Productos",
                        principalColumn: "ProductosID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductoSubCategorias_SubCategoriaProductos_SubCategoriasProductosID",
                        column: x => x.SubCategoriasProductosID,
                        principalTable: "SubCategoriaProductos",
                        principalColumn: "SubCategoriasProductosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoSubCategorias_EmpresaID",
                table: "ProductoSubCategorias",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoSubCategorias_ProductosID_SubCategoriasProductosID",
                table: "ProductoSubCategorias",
                columns: new[] { "ProductosID", "SubCategoriasProductosID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductoSubCategorias_SubCategoriasProductosID",
                table: "ProductoSubCategorias",
                column: "SubCategoriasProductosID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ProductoSubCategorias");

            migrationBuilder.AddColumn<int>(
                name: "CategoriasProductosID",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoriasProductosID",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriasProductosID",
                table: "Productos",
                column: "CategoriasProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_SubCategoriasProductosID",
                table: "Productos",
                column: "SubCategoriasProductosID");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_CategoriaProductos_CategoriasProductosID",
                table: "Productos",
                column: "CategoriasProductosID",
                principalTable: "CategoriaProductos",
                principalColumn: "CategoriasProductosID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_SubCategoriaProductos_SubCategoriasProductosID",
                table: "Productos",
                column: "SubCategoriasProductosID",
                principalTable: "SubCategoriaProductos",
                principalColumn: "SubCategoriasProductosID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
