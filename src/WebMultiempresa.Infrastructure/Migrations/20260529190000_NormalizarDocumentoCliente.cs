using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using WebMultiempresa.Infrastructure.Persistence;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(AppDbContext))]
    [Migration("20260529190000_NormalizarDocumentoCliente")]
    public partial class NormalizarDocumentoCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar nuevas columnas
            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "Clientes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TiposDocumentosID",
                table: "Clientes",
                type: "int",
                nullable: true);

            // Migrar datos: CUIT tiene prioridad, DNI como fallback
            migrationBuilder.Sql(@"
                UPDATE Clientes
                SET Documento = Cuit, TiposDocumentosID = 3
                WHERE Cuit IS NOT NULL AND Cuit != '';

                UPDATE Clientes
                SET Documento = Dni, TiposDocumentosID = 1
                WHERE Dni IS NOT NULL AND Dni != ''
                  AND (Cuit IS NULL OR Cuit = '');
            ");

            // FK a TiposDocumentos
            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TiposDocumentosID",
                table: "Clientes",
                column: "TiposDocumentosID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_TiposDocumentos_TiposDocumentosID",
                table: "Clientes",
                column: "TiposDocumentosID",
                principalTable: "TiposDocumentos",
                principalColumn: "TiposDocumentosID",
                onDelete: ReferentialAction.Restrict);

            // Eliminar columnas viejas
            migrationBuilder.DropColumn(name: "Dni",  table: "Clientes");
            migrationBuilder.DropColumn(name: "Cuit", table: "Clientes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Clientes_TiposDocumentos_TiposDocumentosID", table: "Clientes");
            migrationBuilder.DropIndex(name: "IX_Clientes_TiposDocumentosID", table: "Clientes");
            migrationBuilder.DropColumn(name: "Documento",         table: "Clientes");
            migrationBuilder.DropColumn(name: "TiposDocumentosID", table: "Clientes");

            migrationBuilder.AddColumn<string>(name: "Dni",  table: "Clientes", type: "nvarchar(20)", maxLength: 20, nullable: true);
            migrationBuilder.AddColumn<string>(name: "Cuit", table: "Clientes", type: "nvarchar(20)", maxLength: 20, nullable: true);
        }
    }
}
