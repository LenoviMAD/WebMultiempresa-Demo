using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarClienteDocumentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MarcasProductosID",
                table: "Combos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ClienteDocumentos",
                columns: table => new
                {
                    ClienteDocumentosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    TiposDocumentosID = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Validado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteDocumentos", x => x.ClienteDocumentosID);
                    table.ForeignKey(
                        name: "FK_ClienteDocumentos_Clientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "Clientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClienteDocumentos_TiposDocumentos_TiposDocumentosID",
                        column: x => x.TiposDocumentosID,
                        principalTable: "TiposDocumentos",
                        principalColumn: "TiposDocumentosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteDocumentos_ClientesID_EmpresaID_TiposDocumentosID",
                table: "ClienteDocumentos",
                columns: new[] { "ClientesID", "EmpresaID", "TiposDocumentosID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClienteDocumentos_TiposDocumentosID",
                table: "ClienteDocumentos",
                column: "TiposDocumentosID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteDocumentos");

            migrationBuilder.AlterColumn<int>(
                name: "MarcasProductosID",
                table: "Combos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
