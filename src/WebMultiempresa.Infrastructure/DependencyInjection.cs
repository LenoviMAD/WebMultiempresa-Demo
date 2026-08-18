using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebMultiempresa.Application.Commands.Auth;
using WebMultiempresa.Application.Settings;
using WebMultiempresa.Application.Commands.Combos;
using WebMultiempresa.Application.Commands.Empresas;
using WebMultiempresa.Application.Commands.Productos;
using WebMultiempresa.Application.Commands.Usuarios;
using WebMultiempresa.Application.Commands.Vendedores;
using WebMultiempresa.Application.Ports;
using WebMultiempresa.Application.Queries.Clientes;
using WebMultiempresa.Application.Queries.Combos;
using WebMultiempresa.Application.Queries.Empresas;
using WebMultiempresa.Application.Queries.Productos;
using WebMultiempresa.Application.Queries.ListasPrecios;
using WebMultiempresa.Application.Queries.MarcasProductos;
using WebMultiempresa.Application.Queries.FamiliaProductos;
using WebMultiempresa.Application.Queries.CategoriasComerciales;
using WebMultiempresa.Application.Queries.Sucursales;
using WebMultiempresa.Application.Queries.Vendedores;
using WebMultiempresa.Application.Queries.Usuarios;
using WebMultiempresa.Application.Queries.Portal;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;
using WebMultiempresa.Infrastructure.Repositories;
using WebMultiempresa.Infrastructure.Services;

namespace WebMultiempresa.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Settings
        services.Configure<ProductosSettings>(configuration.GetSection(ProductosSettings.SectionName));

        // Tenant/user context — scoped para que cada circuito Blazor tenga el suyo
        services.AddScoped<EmpresaContexto>();
        services.AddScoped<UsuarioContexto>();
        services.AddScoped<VendedorContexto>();

        // EF Core
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sql => sql.MigrationsAssembly("WebMultiempresa.Infrastructure")));

        // Repositorios
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IComboRepository, ComboRepository>();
        services.AddScoped<IProductoRepository, ProductoRepository>();
        services.AddScoped<IMarcaProductoRepository, MarcaProductoRepository>();
        services.AddScoped<IFamiliaProductoRepository, FamiliaProductoRepository>();
        services.AddScoped<IListaPreciosRepository, ListaPreciosRepository>();
        services.AddScoped<IVendedorRepository, VendedorRepository>();
        services.AddScoped<ISucursalRepository, SucursalRepository>();
        services.AddScoped<IActorEstadoLogRepository, ActorEstadoLogRepository>();
        services.AddScoped<IVendedorListaPreciosRepository, VendedorListaPreciosRepository>();
        services.AddScoped<ITipoActorRepository, TipoActorRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IClienteVendedorRepository, ClienteVendedorRepository>();
        services.AddScoped<ICategoriaComercialRepository, CategoriaComercialRepository>();
        services.AddScoped<IVendedorEstrellasDefinicionRepository, VendedorEstrellasDefinicionRepository>();
        services.AddScoped<IVendedorEstrellasCoeficienteRepository, VendedorEstrellasCoeficienteRepository>();
        services.AddScoped<IVendedorEstrellaDiariaRepository, VendedorEstrellaDiariaRepository>();
        services.AddScoped<IVendedorEstadisticaRepository, VendedorEstadisticaRepository>();

        // Servicios
        services.AddSingleton<IAuthService, JwtAuthService>();
        services.AddScoped<ICurrentEmpresaContext, BlazorCurrentEmpresaContext>();
        services.AddScoped<ICurrentUserContext, BlazorCurrentUserContext>();
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();

        // Read ports (SP-backed)
        services.AddScoped<IComboReadPort, ComboReadService>();
        services.AddScoped<IUsuarioReadPort, UsuarioReadService>();

        // Command handlers — Auth
        services.AddScoped<LoginCommandHandler>();
        services.AddScoped<LoginVendedorCommandHandler>();

        // Command handlers — Combos
        services.AddScoped<CrearComboCommandHandler>();
        services.AddScoped<EditarComboCommandHandler>();
        services.AddScoped<BajaComboCommandHandler>();
        services.AddScoped<AgregarVigenciaCombosCommandHandler>();

        // Command handlers — Productos
        services.AddScoped<CrearProductoCommandHandler>();
        services.AddScoped<EditarProductoCommandHandler>();
        services.AddScoped<BajaProductoCommandHandler>();

        // Command handlers — Usuarios
        services.AddScoped<CrearUsuarioCommandHandler>();
        services.AddScoped<EditarUsuarioCommandHandler>();
        services.AddScoped<BajaUsuarioCommandHandler>();

        // Command handlers — Vendedores
        services.AddScoped<CrearVendedorCommandHandler>();
        services.AddScoped<EditarVendedorCommandHandler>();
        services.AddScoped<BajaVendedorCommandHandler>();
        services.AddScoped<ReactivarVendedorCommandHandler>();
        services.AddScoped<AgregarListaVendedorCommandHandler>();
        services.AddScoped<QuitarListaVendedorCommandHandler>();
        services.AddScoped<MarcarListaDefaultVendedorCommandHandler>();
        services.AddScoped<AsignarClientesVendedorCommandHandler>();
        services.AddScoped<QuitarClienteVendedorCommandHandler>();
        services.AddScoped<GuardarEstrellasDefinicionCommandHandler>();
        services.AddScoped<BajaEstrellasDefinicionCommandHandler>();
        services.AddScoped<GuardarEstrellasCoeficienteCommandHandler>();

        // Command handlers — Empresas
        services.AddScoped<CrearEmpresaCommandHandler>();
        services.AddScoped<EditarEmpresaCommandHandler>();
        services.AddScoped<BajaEmpresaCommandHandler>();

        // Query handlers — Combos
        services.AddScoped<ListarCombosQuery>();
        services.AddScoped<ObtenerComboQuery>();

        // Query handlers — Productos
        services.AddScoped<ListarProductosQuery>();
        services.AddScoped<ObtenerProductosParaComboQuery>();
        services.AddScoped<ObtenerProductoQuery>();

        // Query handlers — MarcasProductos / FamiliaProductos
        services.AddScoped<ListarMarcasProductosQuery>();
        services.AddScoped<ListarFamiliaProductosQuery>();

        // Query handlers — Vendedores / ListasPrecios / Sucursales / Clientes / CategoriasComerciales
        services.AddScoped<ListarVendedoresQuery>();
        services.AddScoped<ListarVendedoresDadosDeBajaQuery>();
        services.AddScoped<ObtenerVendedorQuery>();
        services.AddScoped<ListarListasVendedorQuery>();
        services.AddScoped<ListarClientesVendedorQuery>();
        services.AddScoped<ContarActoresActivosPorMesQuery>();
        services.AddScoped<ListarListasPreciosQuery>();
        services.AddScoped<ListarSucursalesQuery>();
        services.AddScoped<ListarClientesQuery>();
        services.AddScoped<ListarCategoriasComercalesQuery>();
        services.AddScoped<ListarEstrellasDefinicionesQuery>();
        services.AddScoped<ListarEstrellasCoeficientesQuery>();
        services.AddScoped<ListarEstrellasConfigQuery>();

        // Query handlers — Portal Vendedor
        services.AddScoped<ObtenerResumenPortalVendedorQuery>();
        services.AddScoped<ObtenerDetalleEstrellaDiariaQuery>();
        services.AddScoped<ListarClientesDiaSiguienteQuery>();

        // Query handlers — Usuarios
        services.AddScoped<ListarUsuariosQuery>();
        services.AddScoped<ObtenerUsuarioQuery>();

        // Query handlers — Empresas
        services.AddScoped<ListarEmpresasQuery>();
        services.AddScoped<ObtenerEmpresaQuery>();

        return services;
    }
}
