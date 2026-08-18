using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenombrarRubrosYMarcas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Combos_Rubros_RubrosID",
                table: "Combos");

            migrationBuilder.DropTable(
                name: "ComboMarcas");

            migrationBuilder.DropTable(
                name: "ComboRubros");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Rubros");

            migrationBuilder.RenameColumn(
                name: "RubrosID",
                table: "Combos",
                newName: "MarcasProductosID");

            migrationBuilder.RenameIndex(
                name: "IX_Combos_RubrosID",
                table: "Combos",
                newName: "IX_Combos_MarcasProductosID");

            // Hacer la columna nullable y limpiar valores legacy (IDs de Rubros ya eliminados)
            migrationBuilder.AlterColumn<int>(
                name: "MarcasProductosID",
                table: "Combos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.Sql("UPDATE [Combos] SET [MarcasProductosID] = NULL");

            migrationBuilder.CreateTable(
                name: "FamiliaProductos",
                columns: table => new
                {
                    FamiliaProductosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamiliaProductos", x => x.FamiliaProductosID);
                    table.ForeignKey(
                        name: "FK_FamiliaProductos_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarcasProductos",
                columns: table => new
                {
                    MarcasProductosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasProductos", x => x.MarcasProductosID);
                    table.ForeignKey(
                        name: "FK_MarcasProductos_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComboFamiliaProductos",
                columns: table => new
                {
                    ComboFamiliaProductosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CombosID = table.Column<int>(type: "int", nullable: false),
                    FamiliaProductosID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboFamiliaProductos", x => x.ComboFamiliaProductosID);
                    table.ForeignKey(
                        name: "FK_ComboFamiliaProductos_Combos_CombosID",
                        column: x => x.CombosID,
                        principalTable: "Combos",
                        principalColumn: "CombosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboFamiliaProductos_FamiliaProductos_FamiliaProductosID",
                        column: x => x.FamiliaProductosID,
                        principalTable: "FamiliaProductos",
                        principalColumn: "FamiliaProductosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComboMarcasProductos",
                columns: table => new
                {
                    ComboMarcasProductosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CombosID = table.Column<int>(type: "int", nullable: false),
                    MarcasProductosID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboMarcasProductos", x => x.ComboMarcasProductosID);
                    table.ForeignKey(
                        name: "FK_ComboMarcasProductos_Combos_CombosID",
                        column: x => x.CombosID,
                        principalTable: "Combos",
                        principalColumn: "CombosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboMarcasProductos_MarcasProductos_MarcasProductosID",
                        column: x => x.MarcasProductosID,
                        principalTable: "MarcasProductos",
                        principalColumn: "MarcasProductosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboFamiliaProductos_CombosID_FamiliaProductosID",
                table: "ComboFamiliaProductos",
                columns: new[] { "CombosID", "FamiliaProductosID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComboFamiliaProductos_FamiliaProductosID",
                table: "ComboFamiliaProductos",
                column: "FamiliaProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboMarcasProductos_CombosID_MarcasProductosID",
                table: "ComboMarcasProductos",
                columns: new[] { "CombosID", "MarcasProductosID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComboMarcasProductos_MarcasProductosID",
                table: "ComboMarcasProductos",
                column: "MarcasProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_FamiliaProductos_EmpresaID",
                table: "FamiliaProductos",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_MarcasProductos_EmpresaID",
                table: "MarcasProductos",
                column: "EmpresaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Combos_MarcasProductos_MarcasProductosID",
                table: "Combos",
                column: "MarcasProductosID",
                principalTable: "MarcasProductos",
                principalColumn: "MarcasProductosID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Combos_MarcasProductos_MarcasProductosID",
                table: "Combos");

            migrationBuilder.DropTable(
                name: "ComboFamiliaProductos");

            migrationBuilder.DropTable(
                name: "ComboMarcasProductos");

            migrationBuilder.DropTable(
                name: "FamiliaProductos");

            migrationBuilder.DropTable(
                name: "MarcasProductos");

            migrationBuilder.RenameColumn(
                name: "MarcasProductosID",
                table: "Combos",
                newName: "RubrosID");

            migrationBuilder.RenameIndex(
                name: "IX_Combos_MarcasProductosID",
                table: "Combos",
                newName: "IX_Combos_RubrosID");

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    MarcasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.MarcasID);
                    table.ForeignKey(
                        name: "FK_Marcas_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rubros",
                columns: table => new
                {
                    RubrosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubros", x => x.RubrosID);
                    table.ForeignKey(
                        name: "FK_Rubros_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComboMarcas",
                columns: table => new
                {
                    ComboMarcasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarcasID = table.Column<int>(type: "int", nullable: false),
                    CombosID = table.Column<int>(type: "int", nullable: false)
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
                    RubrosID = table.Column<int>(type: "int", nullable: false),
                    CombosID = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_EmpresaID",
                table: "Marcas",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Rubros_EmpresaID",
                table: "Rubros",
                column: "EmpresaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Combos_Rubros_RubrosID",
                table: "Combos",
                column: "RubrosID",
                principalTable: "Rubros",
                principalColumn: "RubrosID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
