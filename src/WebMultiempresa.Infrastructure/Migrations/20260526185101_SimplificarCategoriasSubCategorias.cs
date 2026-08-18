using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SimplificarCategoriasSubCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertaDeEdad",
                table: "SubCategoriaProductos");

            migrationBuilder.DropColumn(
                name: "EmojiWhatap",
                table: "SubCategoriaProductos");

            migrationBuilder.DropColumn(
                name: "NivelDeImportancia",
                table: "SubCategoriaProductos");

            migrationBuilder.DropColumn(
                name: "UrlImagenMenu",
                table: "SubCategoriaProductos");

            migrationBuilder.DropColumn(
                name: "EmojiWhatap",
                table: "CategoriaProductos");

            migrationBuilder.DropColumn(
                name: "NivelDeImportancia",
                table: "CategoriaProductos");

            migrationBuilder.DropColumn(
                name: "UrlImagenMenu",
                table: "CategoriaProductos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AlertaDeEdad",
                table: "SubCategoriaProductos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EmojiWhatap",
                table: "SubCategoriaProductos",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NivelDeImportancia",
                table: "SubCategoriaProductos",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UrlImagenMenu",
                table: "SubCategoriaProductos",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmojiWhatap",
                table: "CategoriaProductos",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NivelDeImportancia",
                table: "CategoriaProductos",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UrlImagenMenu",
                table: "CategoriaProductos",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: true);
        }
    }
}
