using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMultiempresa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aplicaciones",
                columns: table => new
                {
                    AplicacionesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplicaciones", x => x.AplicacionesID);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    EmpresaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    KeyConexion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.EmpresaID);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    PlanesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AplicacionesID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.PlanesID);
                    table.ForeignKey(
                        name: "FK_Planes_Aplicaciones_AplicacionesID",
                        column: x => x.AplicacionesID,
                        principalTable: "Aplicaciones",
                        principalColumn: "AplicacionesID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TiposActores",
                columns: table => new
                {
                    TiposActoresID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AplicacionesID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposActores", x => x.TiposActoresID);
                    table.ForeignKey(
                        name: "FK_TiposActores_Aplicaciones_AplicacionesID",
                        column: x => x.AplicacionesID,
                        principalTable: "Aplicaciones",
                        principalColumn: "AplicacionesID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClientesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Localidad = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Cuit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Latitud = table.Column<decimal>(type: "decimal(10,7)", nullable: true),
                    Longitud = table.Column<decimal>(type: "decimal(11,7)", nullable: true),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClientesID);
                    table.ForeignKey(
                        name: "FK_Clientes_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaAplicaciones",
                columns: table => new
                {
                    EmpresaAplicacionesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    AplicacionesID = table.Column<int>(type: "int", nullable: false),
                    FechaActivacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaAplicaciones", x => x.EmpresaAplicacionesID);
                    table.ForeignKey(
                        name: "FK_EmpresaAplicaciones_Aplicaciones_AplicacionesID",
                        column: x => x.AplicacionesID,
                        principalTable: "Aplicaciones",
                        principalColumn: "AplicacionesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpresaAplicaciones_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ListasPrecios",
                columns: table => new
                {
                    ListasPreciosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasPrecios", x => x.ListasPreciosID);
                    table.ForeignKey(
                        name: "FK_ListasPrecios_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    MarcasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.MarcasID);
                    table.ForeignKey(
                        name: "FK_Marcas_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    PedidosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    JsonPedido = table.Column<string>(type: "varchar(max)", nullable: false),
                    HoraGuardada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraDescargada = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidosID);
                    table.ForeignKey(
                        name: "FK_Pedidos_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductosID);
                    table.ForeignKey(
                        name: "FK_Productos_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rubros",
                columns: table => new
                {
                    RubrosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubros", x => x.RubrosID);
                    table.ForeignKey(
                        name: "FK_Rubros_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    SucursalesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.SucursalesID);
                    table.ForeignKey(
                        name: "FK_Sucursales_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuariosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Rol = table.Column<byte>(type: "tinyint", nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuariosID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID");
                });

            migrationBuilder.CreateTable(
                name: "Vendedores",
                columns: table => new
                {
                    VendedoresID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedores", x => x.VendedoresID);
                    table.ForeignKey(
                        name: "FK_Vendedores_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaPlanes",
                columns: table => new
                {
                    EmpresaPlanesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    PlanesID = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaPlanes", x => x.EmpresaPlanesID);
                    table.ForeignKey(
                        name: "FK_EmpresaPlanes_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmpresaPlanes_Planes_PlanesID",
                        column: x => x.PlanesID,
                        principalTable: "Planes",
                        principalColumn: "PlanesID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanCapacidades",
                columns: table => new
                {
                    PlanCapacidadesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanesID = table.Column<int>(type: "int", nullable: false),
                    TiposActoresID = table.Column<int>(type: "int", nullable: false),
                    MaxCapacidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanCapacidades", x => x.PlanCapacidadesID);
                    table.ForeignKey(
                        name: "FK_PlanCapacidades_Planes_PlanesID",
                        column: x => x.PlanesID,
                        principalTable: "Planes",
                        principalColumn: "PlanesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanCapacidades_TiposActores_TiposActoresID",
                        column: x => x.TiposActoresID,
                        principalTable: "TiposActores",
                        principalColumn: "TiposActoresID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClienteListasPrecios",
                columns: table => new
                {
                    ClienteListasPreciosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    ListasPreciosID = table.Column<int>(type: "int", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    EsPrincipal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteListasPrecios", x => x.ClienteListasPreciosID);
                    table.ForeignKey(
                        name: "FK_ClienteListasPrecios_Clientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "Clientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClienteListasPrecios_ListasPrecios_ListasPreciosID",
                        column: x => x.ListasPreciosID,
                        principalTable: "ListasPrecios",
                        principalColumn: "ListasPreciosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductoPrecios",
                columns: table => new
                {
                    ProductoPreciosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductosID = table.Column<int>(type: "int", nullable: false),
                    ListasPreciosID = table.Column<int>(type: "int", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoPrecios", x => x.ProductoPreciosID);
                    table.ForeignKey(
                        name: "FK_ProductoPrecios_ListasPrecios_ListasPreciosID",
                        column: x => x.ListasPreciosID,
                        principalTable: "ListasPrecios",
                        principalColumn: "ListasPreciosID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductoPrecios_Productos_ProductosID",
                        column: x => x.ProductosID,
                        principalTable: "Productos",
                        principalColumn: "ProductosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Combos",
                columns: table => new
                {
                    CombosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    RubrosID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NombreAlternativo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    CantidadFacturada = table.Column<int>(type: "int", nullable: false),
                    CantidadPorFactura = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CantidadDinamica = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CantidadDinamicaMaxima = table.Column<int>(type: "int", nullable: false),
                    CantidadSinCargo = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false, defaultValue: 1m),
                    ImporteProductosFuera = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PorcentajeComision = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVigencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TodosLosVendedores = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    TodasLasListasPrecios = table.Column<bool>(type: "bit", nullable: false),
                    TodasLasSucursales = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ClientesNumericos = table.Column<bool>(type: "bit", nullable: false),
                    ClientesAlfaNumericos = table.Column<bool>(type: "bit", nullable: false),
                    EsEstrategico = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SoloNoCompradores = table.Column<bool>(type: "bit", nullable: false),
                    SoloNoCompradoresDesde = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ComboDinamico = table.Column<bool>(type: "bit", nullable: false),
                    EsDeIntroduccion = table.Column<bool>(type: "bit", nullable: false),
                    NoImprimir = table.Column<bool>(type: "bit", nullable: false),
                    ImpresionResumida = table.Column<bool>(type: "bit", nullable: false),
                    UsarDescuentoClientes = table.Column<bool>(type: "bit", nullable: false),
                    ValidarPartido = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CupoTotal = table.Column<int>(type: "int", nullable: true),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combos", x => x.CombosID);
                    table.ForeignKey(
                        name: "FK_Combos_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Combos_Rubros_RubrosID",
                        column: x => x.RubrosID,
                        principalTable: "Rubros",
                        principalColumn: "RubrosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioAppPermisos",
                columns: table => new
                {
                    UsuarioAppPermisosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuariosID = table.Column<int>(type: "int", nullable: false),
                    AplicacionesID = table.Column<int>(type: "int", nullable: false),
                    Permiso = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioAppPermisos", x => x.UsuarioAppPermisosID);
                    table.ForeignKey(
                        name: "FK_UsuarioAppPermisos_Aplicaciones_AplicacionesID",
                        column: x => x.AplicacionesID,
                        principalTable: "Aplicaciones",
                        principalColumn: "AplicacionesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioAppPermisos_Usuarios_UsuariosID",
                        column: x => x.UsuariosID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuariosID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClienteVendedores",
                columns: table => new
                {
                    ClienteVendedoresID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientesID = table.Column<int>(type: "int", nullable: false),
                    VendedoresID = table.Column<int>(type: "int", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    Baja = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteVendedores", x => x.ClienteVendedoresID);
                    table.ForeignKey(
                        name: "FK_ClienteVendedores_Clientes_ClientesID",
                        column: x => x.ClientesID,
                        principalTable: "Clientes",
                        principalColumn: "ClientesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClienteVendedores_Vendedores_VendedoresID",
                        column: x => x.VendedoresID,
                        principalTable: "Vendedores",
                        principalColumn: "VendedoresID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GPS_Posiciones",
                columns: table => new
                {
                    GPS_PosicionesID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    AplicacionesID = table.Column<int>(type: "int", nullable: false),
                    VendedoresID = table.Column<int>(type: "int", nullable: false),
                    Latitud = table.Column<decimal>(type: "decimal(10,7)", nullable: false),
                    Longitud = table.Column<decimal>(type: "decimal(11,7)", nullable: false),
                    VelocidadKmh = table.Column<decimal>(type: "decimal(5,1)", nullable: true),
                    AcuracyMetros = table.Column<int>(type: "int", nullable: true),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPS_Posiciones", x => x.GPS_PosicionesID);
                    table.ForeignKey(
                        name: "FK_GPS_Posiciones_Aplicaciones_AplicacionesID",
                        column: x => x.AplicacionesID,
                        principalTable: "Aplicaciones",
                        principalColumn: "AplicacionesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GPS_Posiciones_Vendedores_VendedoresID",
                        column: x => x.VendedoresID,
                        principalTable: "Vendedores",
                        principalColumn: "VendedoresID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComboFechas",
                columns: table => new
                {
                    ComboFechasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CombosID = table.Column<int>(type: "int", nullable: false),
                    FechaInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboFechas", x => x.ComboFechasID);
                    table.ForeignKey(
                        name: "FK_ComboFechas_Combos_CombosID",
                        column: x => x.CombosID,
                        principalTable: "Combos",
                        principalColumn: "CombosID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboItems",
                columns: table => new
                {
                    ComboItemsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CombosID = table.Column<int>(type: "int", nullable: false),
                    ProductosID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Tipo = table.Column<byte>(type: "tinyint", nullable: false),
                    Descuento1 = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Descuento2 = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    NroGrupoDinamico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboItems", x => x.ComboItemsID);
                    table.ForeignKey(
                        name: "FK_ComboItems_Combos_CombosID",
                        column: x => x.CombosID,
                        principalTable: "Combos",
                        principalColumn: "CombosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboItems_Productos_ProductosID",
                        column: x => x.ProductosID,
                        principalTable: "Productos",
                        principalColumn: "ProductosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComboListasPrecios",
                columns: table => new
                {
                    ComboListaPreciosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CombosID = table.Column<int>(type: "int", nullable: false),
                    ListasPreciosID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboListasPrecios", x => x.ComboListaPreciosID);
                    table.ForeignKey(
                        name: "FK_ComboListasPrecios_Combos_CombosID",
                        column: x => x.CombosID,
                        principalTable: "Combos",
                        principalColumn: "CombosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboListasPrecios_ListasPrecios_ListasPreciosID",
                        column: x => x.ListasPreciosID,
                        principalTable: "ListasPrecios",
                        principalColumn: "ListasPreciosID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComboSucursales",
                columns: table => new
                {
                    ComboSucursalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CombosID = table.Column<int>(type: "int", nullable: false),
                    SucursalesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboSucursales", x => x.ComboSucursalID);
                    table.ForeignKey(
                        name: "FK_ComboSucursales_Combos_CombosID",
                        column: x => x.CombosID,
                        principalTable: "Combos",
                        principalColumn: "CombosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboSucursales_Sucursales_SucursalesID",
                        column: x => x.SucursalesID,
                        principalTable: "Sucursales",
                        principalColumn: "SucursalesID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComboVendedores",
                columns: table => new
                {
                    ComboVendedorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CombosID = table.Column<int>(type: "int", nullable: false),
                    VendedoresID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboVendedores", x => x.ComboVendedorID);
                    table.ForeignKey(
                        name: "FK_ComboVendedores_Combos_CombosID",
                        column: x => x.CombosID,
                        principalTable: "Combos",
                        principalColumn: "CombosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboVendedores_Vendedores_VendedoresID",
                        column: x => x.VendedoresID,
                        principalTable: "Vendedores",
                        principalColumn: "VendedoresID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClienteListasPrecios_ClientesID_ListasPreciosID",
                table: "ClienteListasPrecios",
                columns: new[] { "ClientesID", "ListasPreciosID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClienteListasPrecios_ListasPreciosID",
                table: "ClienteListasPrecios",
                column: "ListasPreciosID");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EmpresaID",
                table: "Clientes",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteVendedores_ClientesID_VendedoresID",
                table: "ClienteVendedores",
                columns: new[] { "ClientesID", "VendedoresID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClienteVendedores_VendedoresID",
                table: "ClienteVendedores",
                column: "VendedoresID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboFechas_CombosID",
                table: "ComboFechas",
                column: "CombosID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboItems_CombosID",
                table: "ComboItems",
                column: "CombosID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboItems_ProductosID",
                table: "ComboItems",
                column: "ProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboListasPrecios_CombosID",
                table: "ComboListasPrecios",
                column: "CombosID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboListasPrecios_ListasPreciosID",
                table: "ComboListasPrecios",
                column: "ListasPreciosID");

            migrationBuilder.CreateIndex(
                name: "IX_Combos_EmpresaID_Codigo",
                table: "Combos",
                columns: new[] { "EmpresaID", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Combos_RubrosID",
                table: "Combos",
                column: "RubrosID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboSucursales_CombosID",
                table: "ComboSucursales",
                column: "CombosID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboSucursales_SucursalesID",
                table: "ComboSucursales",
                column: "SucursalesID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboVendedores_CombosID",
                table: "ComboVendedores",
                column: "CombosID");

            migrationBuilder.CreateIndex(
                name: "IX_ComboVendedores_VendedoresID",
                table: "ComboVendedores",
                column: "VendedoresID");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaAplicaciones_AplicacionesID",
                table: "EmpresaAplicaciones",
                column: "AplicacionesID");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaAplicaciones_EmpresaID_AplicacionesID",
                table: "EmpresaAplicaciones",
                columns: new[] { "EmpresaID", "AplicacionesID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaPlanes_EmpresaID",
                table: "EmpresaPlanes",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaPlanes_PlanesID",
                table: "EmpresaPlanes",
                column: "PlanesID");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_KeyConexion",
                table: "Empresas",
                column: "KeyConexion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GPS_Posiciones_AplicacionesID",
                table: "GPS_Posiciones",
                column: "AplicacionesID");

            migrationBuilder.CreateIndex(
                name: "IX_GPS_Posiciones_EmpresaID_VendedoresID_FechaHora",
                table: "GPS_Posiciones",
                columns: new[] { "EmpresaID", "VendedoresID", "FechaHora" });

            migrationBuilder.CreateIndex(
                name: "IX_GPS_Posiciones_VendedoresID",
                table: "GPS_Posiciones",
                column: "VendedoresID");

            migrationBuilder.CreateIndex(
                name: "IX_ListasPrecios_EmpresaID",
                table: "ListasPrecios",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_EmpresaID",
                table: "Marcas",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EmpresaID",
                table: "Pedidos",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanCapacidades_PlanesID_TiposActoresID",
                table: "PlanCapacidades",
                columns: new[] { "PlanesID", "TiposActoresID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanCapacidades_TiposActoresID",
                table: "PlanCapacidades",
                column: "TiposActoresID");

            migrationBuilder.CreateIndex(
                name: "IX_Planes_AplicacionesID",
                table: "Planes",
                column: "AplicacionesID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoPrecios_ListasPreciosID",
                table: "ProductoPrecios",
                column: "ListasPreciosID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoPrecios_ProductosID_ListasPreciosID",
                table: "ProductoPrecios",
                columns: new[] { "ProductosID", "ListasPreciosID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_EmpresaID",
                table: "Productos",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Rubros_EmpresaID",
                table: "Rubros",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursales_EmpresaID",
                table: "Sucursales",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_TiposActores_AplicacionesID_Codigo",
                table: "TiposActores",
                columns: new[] { "AplicacionesID", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAppPermisos_AplicacionesID",
                table: "UsuarioAppPermisos",
                column: "AplicacionesID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAppPermisos_UsuariosID_AplicacionesID_Permiso",
                table: "UsuarioAppPermisos",
                columns: new[] { "UsuariosID", "AplicacionesID", "Permiso" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpresaID",
                table: "Usuarios",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendedores_EmpresaID",
                table: "Vendedores",
                column: "EmpresaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClienteListasPrecios");

            migrationBuilder.DropTable(
                name: "ClienteVendedores");

            migrationBuilder.DropTable(
                name: "ComboFechas");

            migrationBuilder.DropTable(
                name: "ComboItems");

            migrationBuilder.DropTable(
                name: "ComboListasPrecios");

            migrationBuilder.DropTable(
                name: "ComboSucursales");

            migrationBuilder.DropTable(
                name: "ComboVendedores");

            migrationBuilder.DropTable(
                name: "EmpresaAplicaciones");

            migrationBuilder.DropTable(
                name: "EmpresaPlanes");

            migrationBuilder.DropTable(
                name: "GPS_Posiciones");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "PlanCapacidades");

            migrationBuilder.DropTable(
                name: "ProductoPrecios");

            migrationBuilder.DropTable(
                name: "UsuarioAppPermisos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "Combos");

            migrationBuilder.DropTable(
                name: "Vendedores");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "TiposActores");

            migrationBuilder.DropTable(
                name: "ListasPrecios");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Rubros");

            migrationBuilder.DropTable(
                name: "Aplicaciones");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
