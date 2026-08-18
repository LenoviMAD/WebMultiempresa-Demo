using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPrecioCostoProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioCosto",
                table: "Productos",
                type: "decimal(18,4)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioCosto",
                table: "Productos");
        }
    }
}
