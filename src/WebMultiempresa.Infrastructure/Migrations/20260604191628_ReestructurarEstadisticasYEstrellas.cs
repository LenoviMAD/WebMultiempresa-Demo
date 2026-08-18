using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReestructurarEstadisticasYEstrellas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ══════════════════════════════════════════════════════════════════════
            // PASO 1 — Soltar FK y el índice único viejo de VendedorEstrellasDiarias
            //          (necesario antes de tocar la tabla)
            // ══════════════════════════════════════════════════════════════════════
            migrationBuilder.DropForeignKey(
                name: "FK_VendedorEstrellasDiarias_VendedorEstadisticas_VendedorEstadisticasID",
                table: "VendedorEstrellasDiarias");

            migrationBuilder.DropIndex(
                name: "IX_VendedorEstrellasDiarias_VendedorEstadisticasID_VendedorEstrellasDefinicionesID",
                table: "VendedorEstrellasDiarias");

            // ══════════════════════════════════════════════════════════════════════
            // PASO 2 — Agregar las nuevas columnas como nullable para poder poblarlas
            // ══════════════════════════════════════════════════════════════════════
            migrationBuilder.AddColumn<int>(
                name: "VendedoresID",
                table: "VendedorEstrellasDiarias",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "VendedorEstrellasDiarias",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Baja",
                table: "VendedorEstrellasDiarias",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "VendedorEstrellasDiarias",
                type: "decimal(18,4)",
                nullable: true);

            // ══════════════════════════════════════════════════════════════════════
            // PASO 3 — Data migration: poblar desde VendedorEstadisticas
            //          Valor toma el valor histórico de MetricaDiaria (la métrica real)
            // ══════════════════════════════════════════════════════════════════════
            migrationBuilder.Sql(@"
                UPDATE ed
                SET ed.VendedoresID = e.VendedoresID,
                    ed.Fecha        = e.Fecha,
                    ed.Baja         = 0,
                    ed.Valor        = ed.MetricaDiaria
                FROM VendedorEstrellasDiarias ed
                INNER JOIN VendedorEstadisticas e
                    ON e.VendedorEstadisticasID = ed.VendedorEstadisticasID;
            ");

            // ══════════════════════════════════════════════════════════════════════
            // PASO 4 — Soltar columnas que ya no se necesitan en VendedorEstrellasDiarias
            // ══════════════════════════════════════════════════════════════════════
            migrationBuilder.DropColumn(
                name: "MetricaDiaria",
                table: "VendedorEstrellasDiarias");

            migrationBuilder.DropColumn(
                name: "MetricaMensual",
                table: "VendedorEstrellasDiarias");

            migrationBuilder.DropColumn(
                name: "VendedorEstadisticasID",
                table: "VendedorEstrellasDiarias");

            // ══════════════════════════════════════════════════════════════════════
            // PASO 5 — Convertir las nuevas columnas a NOT NULL
            // ══════════════════════════════════════════════════════════════════════
            migrationBuilder.AlterColumn<int>(
                name: "VendedoresID",
                table: "VendedorEstrellasDiarias",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "VendedorEstrellasDiarias",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Baja",
                table: "VendedorEstrellasDiarias",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "VendedorEstrellasDiarias",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            // ══════════════════════════════════════════════════════════════════════
            // PASO 6 — Nuevo índice único + FK a Vendedores en VendedorEstrellasDiarias
            // ══════════════════════════════════════════════════════════════════════
            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstrellasDiarias_VendedoresID_EmpresaID_Fecha_VendedorEstrellasDefinicionesID",
                table: "VendedorEstrellasDiarias",
                columns: new[] { "VendedoresID", "EmpresaID", "Fecha", "VendedorEstrellasDefinicionesID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VendedorEstrellasDiarias_Vendedores_VendedoresID",
                table: "VendedorEstrellasDiarias",
                column: "VendedoresID",
                principalTable: "Vendedores",
                principalColumn: "VendedoresID",
                onDelete: ReferentialAction.Restrict);

            // ══════════════════════════════════════════════════════════════════════
            // PASO 7 — VendedorEstadisticas: eliminar las 15 columnas del CSV
            // ══════════════════════════════════════════════════════════════════════
            migrationBuilder.DropColumn(name: "CajasActual",              table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "CajasAnioAnterior",        table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "ClientesConVentas",        table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "ClientesConVisitas",       table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "ClientesEnCartera",        table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "ClientesEnCarteraDelDia",  table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "ComisionPorDia",           table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "FechaYHoraPrimerCliente",  table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "HorasEnLaRuta",            table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "MarcasDisponiblesDelDia",  table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "MixMarcas",                table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "PrimerClienteIDVisitado",  table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "TotalDevolucion",          table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "TotalVenta",               table: "VendedorEstadisticas");
            migrationBuilder.DropColumn(name: "ValesPorDia",              table: "VendedorEstadisticas");

            // ══════════════════════════════════════════════════════════════════════
            // PASO 8 — VendedorEstadisticas: actualizar índice único con EmpresaID
            // ══════════════════════════════════════════════════════════════════════
            migrationBuilder.DropIndex(
                name: "IX_VendedorEstadisticas_VendedoresID_Fecha",
                table: "VendedorEstadisticas");

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstadisticas_VendedoresID_EmpresaID_Fecha",
                table: "VendedorEstadisticas",
                columns: new[] { "VendedoresID", "EmpresaID", "Fecha" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendedorEstrellasDiarias_Vendedores_VendedoresID",
                table: "VendedorEstrellasDiarias");

            migrationBuilder.DropIndex(
                name: "IX_VendedorEstrellasDiarias_VendedoresID_EmpresaID_Fecha_VendedorEstrellasDefinicionesID",
                table: "VendedorEstrellasDiarias");

            migrationBuilder.DropIndex(
                name: "IX_VendedorEstadisticas_VendedoresID_EmpresaID_Fecha",
                table: "VendedorEstadisticas");

            migrationBuilder.DropColumn(name: "Baja",        table: "VendedorEstrellasDiarias");
            migrationBuilder.DropColumn(name: "Fecha",       table: "VendedorEstrellasDiarias");
            migrationBuilder.DropColumn(name: "Valor",       table: "VendedorEstrellasDiarias");
            migrationBuilder.DropColumn(name: "VendedoresID",table: "VendedorEstrellasDiarias");

            migrationBuilder.AddColumn<decimal>(name: "MetricaDiaria",          table: "VendedorEstrellasDiarias", type: "decimal(18,4)", nullable: false, defaultValue: 0m);
            migrationBuilder.AddColumn<decimal>(name: "MetricaMensual",         table: "VendedorEstrellasDiarias", type: "decimal(18,4)", nullable: false, defaultValue: 0m);
            migrationBuilder.AddColumn<long>   (name: "VendedorEstadisticasID", table: "VendedorEstrellasDiarias", type: "bigint",        nullable: false, defaultValue: 0L);

            migrationBuilder.AddColumn<decimal> (name: "CajasActual",             table: "VendedorEstadisticas", type: "decimal(18,2)", nullable: true);
            migrationBuilder.AddColumn<decimal> (name: "CajasAnioAnterior",       table: "VendedorEstadisticas", type: "decimal(18,2)", nullable: true);
            migrationBuilder.AddColumn<int>     (name: "ClientesConVentas",       table: "VendedorEstadisticas", type: "int",           nullable: false, defaultValue: 0);
            migrationBuilder.AddColumn<int>     (name: "ClientesConVisitas",      table: "VendedorEstadisticas", type: "int",           nullable: false, defaultValue: 0);
            migrationBuilder.AddColumn<int>     (name: "ClientesEnCartera",       table: "VendedorEstadisticas", type: "int",           nullable: false, defaultValue: 0);
            migrationBuilder.AddColumn<int>     (name: "ClientesEnCarteraDelDia", table: "VendedorEstadisticas", type: "int",           nullable: false, defaultValue: 0);
            migrationBuilder.AddColumn<decimal> (name: "ComisionPorDia",          table: "VendedorEstadisticas", type: "decimal(18,2)", nullable: false, defaultValue: 0m);
            migrationBuilder.AddColumn<DateTime>(name: "FechaYHoraPrimerCliente", table: "VendedorEstadisticas", type: "datetime2",     nullable: true);
            migrationBuilder.AddColumn<decimal> (name: "HorasEnLaRuta",           table: "VendedorEstadisticas", type: "decimal(8,3)",  nullable: false, defaultValue: 0m);
            migrationBuilder.AddColumn<int>     (name: "MarcasDisponiblesDelDia", table: "VendedorEstadisticas", type: "int",           nullable: false, defaultValue: 0);
            migrationBuilder.AddColumn<int>     (name: "MixMarcas",               table: "VendedorEstadisticas", type: "int",           nullable: true);
            migrationBuilder.AddColumn<int>     (name: "PrimerClienteIDVisitado", table: "VendedorEstadisticas", type: "int",           nullable: true);
            migrationBuilder.AddColumn<decimal> (name: "TotalDevolucion",         table: "VendedorEstadisticas", type: "decimal(18,2)", nullable: false, defaultValue: 0m);
            migrationBuilder.AddColumn<decimal> (name: "TotalVenta",              table: "VendedorEstadisticas", type: "decimal(18,2)", nullable: false, defaultValue: 0m);
            migrationBuilder.AddColumn<decimal> (name: "ValesPorDia",             table: "VendedorEstadisticas", type: "decimal(18,2)", nullable: false, defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstrellasDiarias_VendedorEstadisticasID_VendedorEstrellasDefinicionesID",
                table: "VendedorEstrellasDiarias",
                columns: new[] { "VendedorEstadisticasID", "VendedorEstrellasDefinicionesID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendedorEstadisticas_VendedoresID_Fecha",
                table: "VendedorEstadisticas",
                columns: new[] { "VendedoresID", "Fecha" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VendedorEstrellasDiarias_VendedorEstadisticas_VendedorEstadisticasID",
                table: "VendedorEstrellasDiarias",
                column: "VendedorEstadisticasID",
                principalTable: "VendedorEstadisticas",
                principalColumn: "VendedorEstadisticasID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
