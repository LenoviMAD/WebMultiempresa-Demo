using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFechaUltimoReingresoProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUltimoReingreso",
                table: "Productos",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaUltimoReingreso",
                table: "Productos");
        }
    }
}
