using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarClientesPedidosCompleto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pedidos_EmpresaID",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_EmpresaID",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "JsonPedido",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "HoraGuardada",
                table: "Pedidos",
                newName: "FechaCarga");

            migrationBuilder.RenameColumn(
                name: "HoraDescargada",
                table: "Pedidos",
                newName: "FechaTx");

            migrationBuilder.AddColumn<bool>(
                name: "Baja",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ClienteCodigo",
                table: "Pedidos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClienteDireccion",
                table: "Pedidos",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientesID",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Comentario",
                table: "Pedidos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ConsumidorFinal",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeviceID",
                table: "Pedidos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Division",
                table: "Pedidos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstadoArrastre",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Pedidos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitud",
                table: "Pedidos",
                type: "decimal(18,10)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitud",
                table: "Pedidos",
                type: "decimal(18,10)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PedidoCerrado",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResultadoTx",
                table: "Pedidos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "TipoCliente",
                table: "Pedidos",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPedido",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VendedoresID",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitud",
                table: "Clientes",
                type: "decimal(18,10)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,7)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitud",
                table: "Clientes",
                type: "decimal(18,10)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,7)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClaveHash",
                table: "Clientes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoPostal",
                table: "Clientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ConsumidorFinal",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DiaDeVenta",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Dni",
                table: "Clientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Clientes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "ListaDePrecioID",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajePercepcionIB",
                table: "Clientes",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TipoClienteID",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AltaClientesPendientes",
                columns: table => new
                {
                    AltaClientesPendientesID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    TipoUsuario = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TipoDocumento = table.Column<byte>(type: "tinyint", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TelefonoNormalizado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Localidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DireccionEnvio = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Latitud = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    Longitud = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Estado = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    ClientesID = table.Column<int>(type: "int", nullable: true),
                    ErrorDetalle = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FechaConfirmacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaProcesamiento = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AltaClientesPendientes", x => x.AltaClientesPendientesID);
                    table.ForeignKey(
                        name: "FK_AltaClientesPendientes_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PedidosDetalle",
                columns: table => new
                {
                    PedidosDetalleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidosID = table.Column<int>(type: "int", nullable: false),
                    ProductosID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Descuento1 = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0m),
                    Descuento2 = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0m),
                    EsCombo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CombosID = table.Column<int>(type: "int", nullable: true),
                    TipoComboItem = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosDetalle", x => x.PedidosDetalleID);
                    table.ForeignKey(
                        name: "FK_PedidosDetalle_Pedidos_PedidosID",
                        column: x => x.PedidosID,
                        principalTable: "Pedidos",
                        principalColumn: "PedidosID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EmpresaID_ClientesID",
                table: "Pedidos",
                columns: new[] { "EmpresaID", "ClientesID" });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EmpresaID_PedidoCerrado",
                table: "Pedidos",
                columns: new[] { "EmpresaID", "PedidoCerrado" });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EmpresaID_VendedoresID_FechaCarga",
                table: "Pedidos",
                columns: new[] { "EmpresaID", "VendedoresID", "FechaCarga" });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EmpresaID_Codigo",
                table: "Clientes",
                columns: new[] { "EmpresaID", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EmpresaID_ListaDePrecioID",
                table: "Clientes",
                columns: new[] { "EmpresaID", "ListaDePrecioID" });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EmpresaID_TipoClienteID",
                table: "Clientes",
                columns: new[] { "EmpresaID", "TipoClienteID" });

            migrationBuilder.CreateIndex(
                name: "IX_AltaClientesPendientes_EmpresaID_Estado",
                table: "AltaClientesPendientes",
                columns: new[] { "EmpresaID", "Estado" });

            migrationBuilder.CreateIndex(
                name: "IX_AltaClientesPendientes_TelefonoNormalizado",
                table: "AltaClientesPendientes",
                column: "TelefonoNormalizado");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalle_PedidosID",
                table: "PedidosDetalle",
                column: "PedidosID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AltaClientesPendientes");

            migrationBuilder.DropTable(
                name: "PedidosDetalle");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_EmpresaID_ClientesID",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_EmpresaID_PedidoCerrado",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_EmpresaID_VendedoresID_FechaCarga",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_EmpresaID_Codigo",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_EmpresaID_ListaDePrecioID",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_EmpresaID_TipoClienteID",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Baja",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ClienteCodigo",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ClienteDireccion",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ClientesID",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Comentario",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ConsumidorFinal",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "DeviceID",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Division",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "EstadoArrastre",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Latitud",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Longitud",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "PedidoCerrado",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ResultadoTx",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "TipoCliente",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "TotalPedido",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "VendedoresID",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ClaveHash",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ConsumidorFinal",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "DiaDeVenta",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Dni",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ListaDePrecioID",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "PorcentajePercepcionIB",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "TipoClienteID",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "FechaTx",
                table: "Pedidos",
                newName: "HoraDescargada");

            migrationBuilder.RenameColumn(
                name: "FechaCarga",
                table: "Pedidos",
                newName: "HoraGuardada");

            migrationBuilder.AddColumn<string>(
                name: "JsonPedido",
                table: "Pedidos",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitud",
                table: "Clientes",
                type: "decimal(11,7)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,10)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitud",
                table: "Clientes",
                type: "decimal(10,7)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,10)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EmpresaID",
                table: "Pedidos",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EmpresaID",
                table: "Clientes",
                column: "EmpresaID");
        }
    }
}
