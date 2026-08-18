using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCategoriaSubCategoriaProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaProductos",
                columns: table => new
                {
                    CategoriasProductosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UrlImagen = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false, defaultValue: ""),
                    UrlImagenMenu = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    NivelDeImportancia = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, defaultValue: 0m),
                    EmojiWhatap = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaProductos", x => x.CategoriasProductosID);
                    table.ForeignKey(
                        name: "FK_CategoriaProductos_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCategoriaProductos",
                columns: table => new
                {
                    SubCategoriasProductosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UrlImagen = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false, defaultValue: ""),
                    UrlImagenMenu = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    NivelDeImportancia = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, defaultValue: 0m),
                    AlertaDeEdad = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EmojiWhatap = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoriaProductos", x => x.SubCategoriasProductosID);
                    table.ForeignKey(
                        name: "FK_SubCategoriaProductos_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaSubCategoriasRelaciones",
                columns: table => new
                {
                    CategoriaSubCategoriaRelacionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    CategoriasProductosID = table.Column<int>(type: "int", nullable: false),
                    SubCategoriasProductosID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaSubCategoriasRelaciones", x => x.CategoriaSubCategoriaRelacionID);
                    table.ForeignKey(
                        name: "FK_CategoriaSubCategoriasRelaciones_CategoriaProductos_CategoriasProductosID",
                        column: x => x.CategoriasProductosID,
                        principalTable: "CategoriaProductos",
                        principalColumn: "CategoriasProductosID");
                    table.ForeignKey(
                        name: "FK_CategoriaSubCategoriasRelaciones_SubCategoriaProductos_SubCategoriasProductosID",
                        column: x => x.SubCategoriasProductosID,
                        principalTable: "SubCategoriaProductos",
                        principalColumn: "SubCategoriasProductosID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaProductos_EmpresaID",
                table: "CategoriaProductos",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaSubCategoriasRelaciones_CategoriasProductosID",
                table: "CategoriaSubCategoriasRelaciones",
                column: "CategoriasProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaSubCategoriasRelaciones_EmpresaID_CategoriasProductosID_SubCategoriasProductosID",
                table: "CategoriaSubCategoriasRelaciones",
                columns: new[] { "EmpresaID", "CategoriasProductosID", "SubCategoriasProductosID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaSubCategoriasRelaciones_SubCategoriasProductosID",
                table: "CategoriaSubCategoriasRelaciones",
                column: "SubCategoriasProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoriaProductos_EmpresaID",
                table: "SubCategoriaProductos",
                column: "EmpresaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriaSubCategoriasRelaciones");

            migrationBuilder.DropTable(
                name: "CategoriaProductos");

            migrationBuilder.DropTable(
                name: "SubCategoriaProductos");
        }
    }
}
