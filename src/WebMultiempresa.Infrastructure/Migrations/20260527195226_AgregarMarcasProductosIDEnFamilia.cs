using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarMarcasProductosIDEnFamilia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarcasProductosID",
                table: "FamiliaProductos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamiliaProductos_MarcasProductosID",
                table: "FamiliaProductos",
                column: "MarcasProductosID");

            migrationBuilder.AddForeignKey(
                name: "FK_FamiliaProductos_MarcasProductos_MarcasProductosID",
                table: "FamiliaProductos",
                column: "MarcasProductosID",
                principalTable: "MarcasProductos",
                principalColumn: "MarcasProductosID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamiliaProductos_MarcasProductos_MarcasProductosID",
                table: "FamiliaProductos");

            migrationBuilder.DropIndex(
                name: "IX_FamiliaProductos_MarcasProductosID",
                table: "FamiliaProductos");

            migrationBuilder.DropColumn(
                name: "MarcasProductosID",
                table: "FamiliaProductos");
        }
    }
}
