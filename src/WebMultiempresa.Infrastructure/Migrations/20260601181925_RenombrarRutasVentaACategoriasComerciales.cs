using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenombrarRutasVentaACategoriasComerciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Soltar FKs que referencian las tablas a renombrar
            migrationBuilder.DropForeignKey(
                name: "FK_Vendedores_RutasVenta_RutasVentaID",
                table: "Vendedores");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoRutas_RutasVenta_RutasVentaID",
                table: "ProductoRutas");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoRutas_Empresas_EmpresaID",
                table: "ProductoRutas");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoRutas_Productos_ProductosID",
                table: "ProductoRutas");

            // 2. Soltar índices que hacen referencia a columnas que vamos a renombrar
            migrationBuilder.DropIndex(
                name: "IX_ProductoRutas_ProductosID_RutasVentaID",
                table: "ProductoRutas");

            migrationBuilder.DropIndex(
                name: "IX_ProductoRutas_RutasVentaID",
                table: "ProductoRutas");

            migrationBuilder.DropIndex(
                name: "IX_ProductoRutas_EmpresaID",
                table: "ProductoRutas");

            migrationBuilder.DropIndex(
                name: "IX_RutasVenta_EmpresaID",
                table: "RutasVenta");

            migrationBuilder.DropIndex(
                name: "IX_Vendedores_RutasVentaID",
                table: "Vendedores");

            // 3. Renombrar tablas (preserva datos)
            migrationBuilder.RenameTable(
                name: "RutasVenta",
                newName: "CategoriasComerciales");

            migrationBuilder.RenameTable(
                name: "ProductoRutas",
                newName: "ProductoCategoriasComerciales");

            // 4. Renombrar columnas
            migrationBuilder.RenameColumn(
                name: "RutasVentaID",
                table: "CategoriasComerciales",
                newName: "CategoriasComercialesID");

            migrationBuilder.RenameColumn(
                name: "ProductoRutasID",
                table: "ProductoCategoriasComerciales",
                newName: "ProductoCategoriasComercialID");

            migrationBuilder.RenameColumn(
                name: "RutasVentaID",
                table: "ProductoCategoriasComerciales",
                newName: "CategoriasComercialesID");

            migrationBuilder.RenameColumn(
                name: "RutasVentaID",
                table: "Vendedores",
                newName: "CategoriasComercialesID");

            // 5. Renombrar constraint PK (sp_rename directo)
            migrationBuilder.Sql("EXEC sp_rename N'PK_RutasVenta', N'PK_CategoriasComerciales'");
            migrationBuilder.Sql("EXEC sp_rename N'PK_ProductoRutas', N'PK_ProductoCategoriasComerciales'");

            // 6. Recrear índices con los nuevos nombres
            migrationBuilder.CreateIndex(
                name: "IX_CategoriasComerciales_EmpresaID",
                table: "CategoriasComerciales",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoCategoriasComerciales_EmpresaID",
                table: "ProductoCategoriasComerciales",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoCategoriasComerciales_CategoriasComercialesID",
                table: "ProductoCategoriasComerciales",
                column: "CategoriasComercialesID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoCategoriasComerciales_ProductosID_CategoriasComercialesID",
                table: "ProductoCategoriasComerciales",
                columns: new[] { "ProductosID", "CategoriasComercialesID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_CategoriasComercialesID",
                table: "Vendedores",
                column: "CategoriasComercialesID");

            // 7. Recrear FKs con los nuevos nombres
            migrationBuilder.AddForeignKey(
                name: "FK_CategoriasComerciales_Empresas_EmpresaID",
                table: "CategoriasComerciales",
                column: "EmpresaID",
                principalTable: "Empresas",
                principalColumn: "EmpresaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoCategoriasComerciales_CategoriasComerciales_CategoriasComercialesID",
                table: "ProductoCategoriasComerciales",
                column: "CategoriasComercialesID",
                principalTable: "CategoriasComerciales",
                principalColumn: "CategoriasComercialesID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoCategoriasComerciales_Empresas_EmpresaID",
                table: "ProductoCategoriasComerciales",
                column: "EmpresaID",
                principalTable: "Empresas",
                principalColumn: "EmpresaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoCategoriasComerciales_Productos_ProductosID",
                table: "ProductoCategoriasComerciales",
                column: "ProductosID",
                principalTable: "Productos",
                principalColumn: "ProductosID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendedores_CategoriasComerciales_CategoriasComercialesID",
                table: "Vendedores",
                column: "CategoriasComercialesID",
                principalTable: "CategoriasComerciales",
                principalColumn: "CategoriasComercialesID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendedores_CategoriasComerciales_CategoriasComercialesID",
                table: "Vendedores");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoCategoriasComerciales_CategoriasComerciales_CategoriasComercialesID",
                table: "ProductoCategoriasComerciales");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoCategoriasComerciales_Empresas_EmpresaID",
                table: "ProductoCategoriasComerciales");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoCategoriasComerciales_Productos_ProductosID",
                table: "ProductoCategoriasComerciales");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriasComerciales_Empresas_EmpresaID",
                table: "CategoriasComerciales");

            migrationBuilder.DropIndex("IX_CategoriasComerciales_EmpresaID", "CategoriasComerciales");
            migrationBuilder.DropIndex("IX_ProductoCategoriasComerciales_EmpresaID", "ProductoCategoriasComerciales");
            migrationBuilder.DropIndex("IX_ProductoCategoriasComerciales_CategoriasComercialesID", "ProductoCategoriasComerciales");
            migrationBuilder.DropIndex("IX_ProductoCategoriasComerciales_ProductosID_CategoriasComercialesID", "ProductoCategoriasComerciales");
            migrationBuilder.DropIndex("IX_Vendedores_CategoriasComercialesID", "Vendedores");

            migrationBuilder.Sql("EXEC sp_rename N'PK_CategoriasComerciales', N'PK_RutasVenta'");
            migrationBuilder.Sql("EXEC sp_rename N'PK_ProductoCategoriasComerciales', N'PK_ProductoRutas'");

            migrationBuilder.RenameColumn("CategoriasComercialesID", "Vendedores", "RutasVentaID");
            migrationBuilder.RenameColumn("CategoriasComercialesID", "ProductoCategoriasComerciales", "RutasVentaID");
            migrationBuilder.RenameColumn("ProductoCategoriasComercialID", "ProductoCategoriasComerciales", "ProductoRutasID");
            migrationBuilder.RenameColumn("CategoriasComercialesID", "CategoriasComerciales", "RutasVentaID");

            migrationBuilder.RenameTable("ProductoCategoriasComerciales", newName: "ProductoRutas");
            migrationBuilder.RenameTable("CategoriasComerciales", newName: "RutasVenta");

            migrationBuilder.CreateIndex("IX_RutasVenta_EmpresaID", "RutasVenta", "EmpresaID");
            migrationBuilder.CreateIndex("IX_ProductoRutas_EmpresaID", "ProductoRutas", "EmpresaID");
            migrationBuilder.CreateIndex("IX_ProductoRutas_RutasVentaID", "ProductoRutas", "RutasVentaID");
            migrationBuilder.CreateIndex("IX_ProductoRutas_ProductosID_RutasVentaID", "ProductoRutas",
                new[] { "ProductosID", "RutasVentaID" }, unique: true);
            migrationBuilder.CreateIndex("IX_Vendedores_RutasVentaID", "Vendedores", "RutasVentaID");

            migrationBuilder.AddForeignKey("FK_RutasVenta_Empresas_EmpresaID", "RutasVenta", "EmpresaID",
                "Empresas", principalColumn: "EmpresaID", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_ProductoRutas_RutasVenta_RutasVentaID", "ProductoRutas", "RutasVentaID",
                "RutasVenta", principalColumn: "RutasVentaID", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_ProductoRutas_Empresas_EmpresaID", "ProductoRutas", "EmpresaID",
                "Empresas", principalColumn: "EmpresaID", onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_ProductoRutas_Productos_ProductosID", "ProductoRutas", "ProductosID",
                "Productos", principalColumn: "ProductosID", onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey("FK_Vendedores_RutasVenta_RutasVentaID", "Vendedores", "RutasVentaID",
                "RutasVenta", principalColumn: "RutasVentaID", onDelete: ReferentialAction.Restrict);
        }
    }
}
