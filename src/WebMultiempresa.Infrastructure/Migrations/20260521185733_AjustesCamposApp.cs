using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjustesCamposApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OcultarBotonEliminar",
                table: "Vendedores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TiposDeVendedoresID",
                table: "Vendedores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GrupoDinamico",
                table: "PedidosDetalle",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ListasPreciosID",
                table: "PedidosDetalle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnidadesPorBulto",
                table: "PedidosDetalle",
                type: "decimal(18,3)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DistanciaMetros",
                table: "GPS_Posiciones",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModeloDispositivo",
                table: "GPS_Posiciones",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroDispositivo",
                table: "GPS_Posiciones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrigenEnvio",
                table: "GPS_Posiciones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OcultarBotonEliminar",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "TiposDeVendedoresID",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "GrupoDinamico",
                table: "PedidosDetalle");

            migrationBuilder.DropColumn(
                name: "ListasPreciosID",
                table: "PedidosDetalle");

            migrationBuilder.DropColumn(
                name: "UnidadesPorBulto",
                table: "PedidosDetalle");

            migrationBuilder.DropColumn(
                name: "DistanciaMetros",
                table: "GPS_Posiciones");

            migrationBuilder.DropColumn(
                name: "ModeloDispositivo",
                table: "GPS_Posiciones");

            migrationBuilder.DropColumn(
                name: "NumeroDispositivo",
                table: "GPS_Posiciones");

            migrationBuilder.DropColumn(
                name: "OrigenEnvio",
                table: "GPS_Posiciones");
        }
    }
}
