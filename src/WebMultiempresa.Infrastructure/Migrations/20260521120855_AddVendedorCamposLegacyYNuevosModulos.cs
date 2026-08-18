using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVendedorCamposLegacyYNuevosModulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vendedores_EmpresaID",
                table: "Vendedores");

            migrationBuilder.AddColumn<int>(
                name: "CantidadEstrellitas",
                table: "Vendedores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ClaveHash",
                table: "Vendedores",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CoeficienteComision",
                table: "Vendedores",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

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

            migrationBuilder.AddColumn<string>(
                name: "RutaDeVenta",
                table: "Vendedores",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlHomeVendedor",
                table: "Vendedores",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsApp",
                table: "Vendedores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActorEstadoLog",
                columns: table => new
                {
                    ActorEstadoLogID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TiposActoresID = table.Column<int>(type: "int", nullable: false),
                    ActorID = table.Column<int>(type: "int", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    TipoEvento = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorEstadoLog", x => x.ActorEstadoLogID);
                    table.ForeignKey(
                        name: "FK_ActorEstadoLog_TiposActores_TiposActoresID",
                        column: x => x.TiposActoresID,
                        principalTable: "TiposActores",
                        principalColumn: "TiposActoresID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendedorListasPrecios",
                columns: table => new
                {
                    VendedorListasPreciosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendedoresID = table.Column<int>(type: "int", nullable: false),
                    ListasPreciosID = table.Column<int>(type: "int", nullable: false),
                    EsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendedorListasPrecios", x => x.VendedorListasPreciosID);
                    table.ForeignKey(
                        name: "FK_VendedorListasPrecios_ListasPrecios_ListasPreciosID",
                        column: x => x.ListasPreciosID,
                        principalTable: "ListasPrecios",
                        principalColumn: "ListasPreciosID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendedorListasPrecios_Vendedores_VendedoresID",
                        column: x => x.VendedoresID,
                        principalTable: "Vendedores",
                        principalColumn: "VendedoresID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_EmpresaID_Codigo",
                table: "Vendedores",
                columns: new[] { "EmpresaID", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActorEstadoLog_TiposActoresID_EmpresaID_TipoEvento_FechaEvento",
                table: "ActorEstadoLog",
                columns: new[] { "TiposActoresID", "EmpresaID", "TipoEvento", "FechaEvento" })
                .Annotation("SqlServer:Include", new[] { "ActorID" });

            migrationBuilder.CreateIndex(
                name: "IX_VendedorListasPrecios_ListasPreciosID",
                table: "VendedorListasPrecios",
                column: "ListasPreciosID");

            migrationBuilder.CreateIndex(
                name: "IX_VendedorListasPrecios_VendedoresID",
                table: "VendedorListasPrecios",
                column: "VendedoresID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorEstadoLog");

            migrationBuilder.DropTable(
                name: "VendedorListasPrecios");

            migrationBuilder.DropIndex(
                name: "IX_Vendedores_EmpresaID_Codigo",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "CantidadEstrellitas",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "ClaveHash",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "CoeficienteComision",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "JsonDisponibles",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "JsonEstrellas",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "RutaDeVenta",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "UrlHomeVendedor",
                table: "Vendedores");

            migrationBuilder.DropColumn(
                name: "WhatsApp",
                table: "Vendedores");

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_EmpresaID",
                table: "Vendedores",
                column: "EmpresaID");
        }
    }
}
