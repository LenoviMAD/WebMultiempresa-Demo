using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SimplificarEstrellasYEliminarUrlVendedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjetivoDiario",
                table: "VendedorEstrellasDefiniciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ObjetivoDiario",
                table: "VendedorEstrellasDefiniciones",
                type: "decimal(18,4)",
                nullable: true);
        }
    }
}
