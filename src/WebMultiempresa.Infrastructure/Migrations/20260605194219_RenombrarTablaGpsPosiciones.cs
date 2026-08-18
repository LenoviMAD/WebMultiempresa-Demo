using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenombrarTablaGpsPosiciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GPS_Posiciones_Aplicaciones_AplicacionesID",
                table: "GPS_Posiciones");

            migrationBuilder.DropForeignKey(
                name: "FK_GPS_Posiciones_Vendedores_VendedoresID",
                table: "GPS_Posiciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GPS_Posiciones",
                table: "GPS_Posiciones");

            migrationBuilder.RenameTable(
                name: "GPS_Posiciones",
                newName: "GpsPosiciones");

            migrationBuilder.RenameColumn(
                name: "AcuracyMetros",
                table: "GpsPosiciones",
                newName: "AccuracyMetros");

            migrationBuilder.RenameColumn(
                name: "GPS_PosicionesID",
                table: "GpsPosiciones",
                newName: "GpsPosicionesID");

            migrationBuilder.RenameIndex(
                name: "IX_GPS_Posiciones_VendedoresID",
                table: "GpsPosiciones",
                newName: "IX_GpsPosiciones_VendedoresID");

            migrationBuilder.RenameIndex(
                name: "IX_GPS_Posiciones_EmpresaID_VendedoresID_FechaHora",
                table: "GpsPosiciones",
                newName: "IX_GpsPosiciones_EmpresaID_VendedoresID_FechaHora");

            migrationBuilder.RenameIndex(
                name: "IX_GPS_Posiciones_AplicacionesID",
                table: "GpsPosiciones",
                newName: "IX_GpsPosiciones_AplicacionesID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GpsPosiciones",
                table: "GpsPosiciones",
                column: "GpsPosicionesID");

            migrationBuilder.AddForeignKey(
                name: "FK_GpsPosiciones_Aplicaciones_AplicacionesID",
                table: "GpsPosiciones",
                column: "AplicacionesID",
                principalTable: "Aplicaciones",
                principalColumn: "AplicacionesID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GpsPosiciones_Vendedores_VendedoresID",
                table: "GpsPosiciones",
                column: "VendedoresID",
                principalTable: "Vendedores",
                principalColumn: "VendedoresID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GpsPosiciones_Aplicaciones_AplicacionesID",
                table: "GpsPosiciones");

            migrationBuilder.DropForeignKey(
                name: "FK_GpsPosiciones_Vendedores_VendedoresID",
                table: "GpsPosiciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GpsPosiciones",
                table: "GpsPosiciones");

            migrationBuilder.RenameTable(
                name: "GpsPosiciones",
                newName: "GPS_Posiciones");

            migrationBuilder.RenameColumn(
                name: "AccuracyMetros",
                table: "GPS_Posiciones",
                newName: "AcuracyMetros");

            migrationBuilder.RenameColumn(
                name: "GpsPosicionesID",
                table: "GPS_Posiciones",
                newName: "GPS_PosicionesID");

            migrationBuilder.RenameIndex(
                name: "IX_GpsPosiciones_VendedoresID",
                table: "GPS_Posiciones",
                newName: "IX_GPS_Posiciones_VendedoresID");

            migrationBuilder.RenameIndex(
                name: "IX_GpsPosiciones_EmpresaID_VendedoresID_FechaHora",
                table: "GPS_Posiciones",
                newName: "IX_GPS_Posiciones_EmpresaID_VendedoresID_FechaHora");

            migrationBuilder.RenameIndex(
                name: "IX_GpsPosiciones_AplicacionesID",
                table: "GPS_Posiciones",
                newName: "IX_GPS_Posiciones_AplicacionesID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GPS_Posiciones",
                table: "GPS_Posiciones",
                column: "GPS_PosicionesID");

            migrationBuilder.AddForeignKey(
                name: "FK_GPS_Posiciones_Aplicaciones_AplicacionesID",
                table: "GPS_Posiciones",
                column: "AplicacionesID",
                principalTable: "Aplicaciones",
                principalColumn: "AplicacionesID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GPS_Posiciones_Vendedores_VendedoresID",
                table: "GPS_Posiciones",
                column: "VendedoresID",
                principalTable: "Vendedores",
                principalColumn: "VendedoresID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
