using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FiltrarIndicesUnicosEstrellasConBaja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VendedorEstrellasDefiniciones_EmpresaID_NumeroEstrella",
                table: "VendedorEstrellasDefiniciones");

            migrationBuilder.DropIndex(
                name: "IX_VendedorEstrellasCoeficientes_EmpresaID_CantidadEstrellas",
                table: "VendedorEstrellasCoeficientes");

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstrellasDefiniciones_EmpresaID_NumeroEstrella",
                table: "VendedorEstrellasDefiniciones",
                columns: new[] { "EmpresaID", "NumeroEstrella" },
                unique: true,
                filter: "[Baja] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstrellasCoeficientes_EmpresaID_CantidadEstrellas",
                table: "VendedorEstrellasCoeficientes",
                columns: new[] { "EmpresaID", "CantidadEstrellas" },
                unique: true,
                filter: "[Baja] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VendedorEstrellasDefiniciones_EmpresaID_NumeroEstrella",
                table: "VendedorEstrellasDefiniciones");

            migrationBuilder.DropIndex(
                name: "IX_VendedorEstrellasCoeficientes_EmpresaID_CantidadEstrellas",
                table: "VendedorEstrellasCoeficientes");

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstrellasDefiniciones_EmpresaID_NumeroEstrella",
                table: "VendedorEstrellasDefiniciones",
                columns: new[] { "EmpresaID", "NumeroEstrella" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstrellasCoeficientes_EmpresaID_CantidadEstrellas",
                table: "VendedorEstrellasCoeficientes",
                columns: new[] { "EmpresaID", "CantidadEstrellas" },
                unique: true);
        }
    }
}
