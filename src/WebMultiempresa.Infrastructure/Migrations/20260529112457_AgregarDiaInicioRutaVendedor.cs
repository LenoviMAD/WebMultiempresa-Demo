using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDiaInicioRutaVendedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiaInicioRuta",
                table: "Vendedores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaInicioRuta",
                table: "Vendedores");
        }
    }
}
