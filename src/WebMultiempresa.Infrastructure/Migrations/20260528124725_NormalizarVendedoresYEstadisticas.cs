using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NormalizarVendedoresYEstadisticas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadEstrellitas",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "JsonDisponibles",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "JsonEstrellas",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "OcultarBotonEliminar",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "RutaDeVenta",
                table: "Vendedores");

            migrationBuilder.AddColumn<int>(
                name: "RutasVentaID",
                table: "Vendedores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VendedorEstadisticas",
                columns: table => new
                {
                    VendedorEstadisticasID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendedoresID = table.Column<int>(type: "int", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    TotalVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDevolucion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientesConVisitas = table.Column<int>(type: "int", nullable: false),
                    ClientesConVentas = table.Column<int>(type: "int", nullable: false),
                    ClientesEnCartera = table.Column<int>(type: "int", nullable: false),
                    ClientesEnCarteraDelDia = table.Column<int>(type: "int", nullable: false),
                    MixMarcas = table.Column<int>(type: "int", nullable: true),
                    MarcasDisponiblesDelDia = table.Column<int>(type: "int", nullable: false),
                    ValesPorDia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ComisionPorDia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HorasEnLaRuta = table.Column<decimal>(type: "decimal(8,3)", nullable: false),
                    CajasAnioAnterior = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CajasActual = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PrimerClienteIDVisitado = table.Column<int>(type: "int", nullable: true),
                    FechaYHoraPrimerCliente = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CoeficienteComision = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendedorEstadisticas", x => x.VendedorEstadisticasID);
                    table.ForeignKey(
                        name: "FK_VendedorEstadisticas_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendedorEstadisticas_Vendedores_VendedoresID",
                        column: x => x.VendedoresID,
                        principalTable: "Vendedores",
                        principalColumn: "VendedoresID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendedorEstrellasCoeficientes",
                columns: table => new
                {
                    VendedorEstrellasCoeficientesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    CantidadEstrellas = table.Column<byte>(type: "tinyint", nullable: false),
                    CoeficienteComision = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendedorEstrellasCoeficientes", x => x.VendedorEstrellasCoeficientesID);
                    table.ForeignKey(
                        name: "FK_VendedorEstrellasCoeficientes_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendedorEstrellasDefiniciones",
                columns: table => new
                {
                    VendedorEstrellasDefinicionesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    NumeroEstrella = table.Column<byte>(type: "tinyint", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ObjetivoDiario = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    ObjetivoMensual = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendedorEstrellasDefiniciones", x => x.VendedorEstrellasDefinicionesID);
                    table.ForeignKey(
                        name: "FK_VendedorEstrellasDefiniciones_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendedorEstrellasDiarias",
                columns: table => new
                {
                    VendedorEstrellaDiariasID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendedorEstadisticasID = table.Column<long>(type: "bigint", nullable: false),
                    VendedorEstrellasDefinicionesID = table.Column<int>(type: "int", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    EstaEncendida = table.Column<bool>(type: "bit", nullable: false),
                    MetricaDiaria = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    MetricaMensual = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendedorEstrellasDiarias", x => x.VendedorEstrellaDiariasID);
                    table.ForeignKey(
                        name: "FK_VendedorEstrellasDiarias_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendedorEstrellasDiarias_VendedorEstadisticas_VendedorEstadisticasID",
                        column: x => x.VendedorEstadisticasID,
                        principalTable: "VendedorEstadisticas",
                        principalColumn: "VendedorEstadisticasID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendedorEstrellasDiarias_VendedorEstrellasDefiniciones_VendedorEstrellasDefinicionesID",
                        column: x => x.VendedorEstrellasDefinicionesID,
                        principalTable: "VendedorEstrellasDefiniciones",
                        principalColumn: "VendedorEstrellasDefinicionesID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_RutasVentaID",
                table: "Vendedores",
                column: "RutasVentaID");

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstadisticas_EmpresaID",
                table: "VendedorEstadisticas",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstadisticas_VendedoresID_Fecha",
                table: "VendedorEstadisticas",
                columns: new[] { "VendedoresID", "Fecha" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstrellasCoeficientes_EmpresaID_CantidadEstrellas",
                table: "VendedorEstrellasCoeficientes",
                columns: new[] { "EmpresaID", "CantidadEstrellas" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstrellasDefiniciones_EmpresaID_NumeroEstrella",
                table: "VendedorEstrellasDefiniciones",
                columns: new[] { "EmpresaID", "NumeroEstrella" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstrellasDiarias_EmpresaID",
                table: "VendedorEstrellasDiarias",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstrellasDiarias_VendedorEstadisticasID_VendedorEstrellasDefinicionesID",
                table: "VendedorEstrellasDiarias",
                columns: new[] { "VendedorEstadisticasID", "VendedorEstrellasDefinicionesID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstrellasDiarias_VendedorEstrellasDefinicionesID",
                table: "VendedorEstrellasDiarias",
                column: "VendedorEstrellasDefinicionesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendedores_RutasVenta_RutasVentaID",
                table: "Vendedores",
                column: "RutasVentaID",
                principalTable: "RutasVenta",
                principalColumn: "RutasVentaID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendedores_RutasVenta_RutasVentaID",
                table: "Vendedores");

            migrationBuilder.DropTable(
                name: "VendedorEstrellasCoeficientes");

            migrationBuilder.DropTable(
                name: "VendedorEstrellasDiarias");

            migrationBuilder.DropTable(
                name: "VendedorEstadisticas");

            migrationBuilder.DropTable(
                name: "VendedorEstrellasDefiniciones");

            migrationBuilder.DropIndex(
                name: "IX_Vendedores_RutasVentaID",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "RutasVentaID",
                table: "Vendedores");

            migrationBuilder.AddColumn<int>(
                name: "CantidadEstrellitas",
                table: "Vendedores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "JsonDisponibles",
                table: "Vendedores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JsonEstrellas",
                table: "Vendedores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OcultarBotonEliminar",
                table: "Vendedores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RutaDeVenta",
                table: "Vendedores",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
