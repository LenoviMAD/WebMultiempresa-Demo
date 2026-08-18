using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EliminarUrlHomeVendedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlHomeVendedor",
                table: "Vendedores");

            migrationBuilder.AlterColumn<decimal>(
                name: "CoeficienteComision",
                table: "Vendedores",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)",
                oldDefaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CoeficienteComision",
                table: "Vendedores",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)",
                oldDefaultValue: 1m);

            migrationBuilder.AddColumn<string>(
                name: "UrlHomeVendedor",
                table: "Vendedores",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
