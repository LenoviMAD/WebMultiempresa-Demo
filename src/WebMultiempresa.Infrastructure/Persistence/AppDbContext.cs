using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    private readonly EmpresaContexto _empresaContexto;

    public AppDbContext(DbContextOptions<AppDbContext> options, EmpresaContexto empresaContexto)
        : base(options)
    {
        _empresaContexto = empresaContexto;
    }

    // ── Core ──────────────────────────────────────────────────────────────────
    public DbSet<Empresa> Empresas => Set<Empresa>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    // ── Catálogos ─────────────────────────────────────────────────────────────
    public DbSet<MarcaProducto> MarcasProductos => Set<MarcaProducto>();
    public DbSet<FamiliaProducto> FamiliaProductos => Set<FamiliaProducto>();
    public DbSet<Impuesto> Impuestos => Set<Impuesto>();
    public DbSet<TipoDocumento> TiposDocumentos => Set<TipoDocumento>();
    public DbSet<ListaPrecios> ListasPrecios => Set<ListaPrecios>();
    public DbSet<ListasPreciosMontoMinimo> ListasPreciosMontoMinimo => Set<ListasPreciosMontoMinimo>();
    public DbSet<Sucursal> Sucursales => Set<Sucursal>();
    public DbSet<CategoriaProducto> CategoriaProductos => Set<CategoriaProducto>();
    public DbSet<SubCategoriaProducto> SubCategoriaProductos => Set<SubCategoriaProducto>();
    public DbSet<CategoriaSubCategoriaRelacion> CategoriaSubCategoriasRelaciones => Set<CategoriaSubCategoriaRelacion>();

    // ── Combos ────────────────────────────────────────────────────────────────
    public DbSet<Combo> Combos => Set<Combo>();
    public DbSet<ComboItem> ComboItems => Set<ComboItem>();
    public DbSet<ComboFecha> ComboFechas => Set<ComboFecha>();
    public DbSet<ComboVendedor> ComboVendedores => Set<ComboVendedor>();
    public DbSet<ComboListaPrecios> ComboListasPrecios => Set<ComboListaPrecios>();
    public DbSet<ComboSucursal> ComboSucursales => Set<ComboSucursal>();
    public DbSet<ComboMarcaProducto> ComboMarcasProductos => Set<ComboMarcaProducto>();
    public DbSet<ComboFamiliaProducto> ComboFamiliaProductos => Set<ComboFamiliaProducto>();
    public DbSet<ComboLog> ComboLogs => Set<ComboLog>();

    // ── Licenciamiento ────────────────────────────────────────────────────────
    public DbSet<Aplicacion> Aplicaciones => Set<Aplicacion>();
    public DbSet<EmpresaAplicacion> EmpresaAplicaciones => Set<EmpresaAplicacion>();
    public DbSet<TipoActor> TiposActores => Set<TipoActor>();
    public DbSet<Plan> Planes => Set<Plan>();
    public DbSet<PlanCapacidad> PlanCapacidades => Set<PlanCapacidad>();
    public DbSet<EmpresaPlan> EmpresaPlanes => Set<EmpresaPlan>();

    // ── Auth ─────────────────────────────────────────────────────────────────
    public DbSet<UsuarioAppPermiso> UsuarioAppPermisos => Set<UsuarioAppPermiso>();

    // ── Catálogos N:M (reemplazos de anti-patrones legacy) ───────────────────
    public DbSet<ProductoPrecio> ProductoPrecios => Set<ProductoPrecio>();
    public DbSet<ProductoCategoriaComercial> ProductoCategoriasComerciales => Set<ProductoCategoriaComercial>();
    public DbSet<ClienteVendedor> ClienteVendedores => Set<ClienteVendedor>();
    public DbSet<ClienteListaPrecios> ClienteListasPrecios => Set<ClienteListaPrecios>();
    public DbSet<ClienteDocumento> ClienteDocumentos => Set<ClienteDocumento>();

    // ── Categorías comerciales ────────────────────────────────────────────────
    public DbSet<CategoriaComercial> CategoriasComerciales => Set<CategoriaComercial>();

    // ── GPS ───────────────────────────────────────────────────────────────────
    public DbSet<GpsPosicion> GpsPosiciones => Set<GpsPosicion>();

    // ── Vendedores ────────────────────────────────────────────────────────────
    public DbSet<ActorEstadoLog> ActorEstadoLogs => Set<ActorEstadoLog>();
    public DbSet<VendedorListaPrecios> VendedorListasPrecios => Set<VendedorListaPrecios>();
    public DbSet<VendedorEstrellasDefinicion> VendedorEstrellasDefiniciones => Set<VendedorEstrellasDefinicion>();
    public DbSet<VendedorEstrellasCoeficiente> VendedorEstrellasCoeficientes => Set<VendedorEstrellasCoeficiente>();
    public DbSet<VendedorEstadistica> VendedorEstadisticas => Set<VendedorEstadistica>();
    public DbSet<VendedorEstrellaDiaria> VendedorEstrellasDiarias => Set<VendedorEstrellaDiaria>();

    // ── Pedidos ───────────────────────────────────────────────────────────────
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<PedidoDetalle> PedidosDetalle => Set<PedidoDetalle>();
    public DbSet<AltaClientePendiente> AltaClientesPendientes => Set<AltaClientePendiente>();

    // ── Legacy (solo lectura desde la webapp) ─────────────────────────────────
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Vendedor> Vendedores => Set<Vendedor>();
    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Global Query Filters — aislamiento por empresa
        // SuperAdmin (EmpresaID null) no tiene filtro: ve todo

        // Catálogos gestionados
        modelBuilder.Entity<MarcaProducto>().HasQueryFilter(r =>
            _empresaContexto.EmpresaID == null || r.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<FamiliaProducto>().HasQueryFilter(m =>
            _empresaContexto.EmpresaID == null || m.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<Impuesto>().HasQueryFilter(i =>
            _empresaContexto.EmpresaID == null || i.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<ListaPrecios>().HasQueryFilter(l =>
            _empresaContexto.EmpresaID == null || l.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<ListasPreciosMontoMinimo>().HasQueryFilter(m =>
            _empresaContexto.EmpresaID == null || m.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<Sucursal>().HasQueryFilter(s =>
            _empresaContexto.EmpresaID == null || s.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<CategoriaProducto>().HasQueryFilter(c =>
            _empresaContexto.EmpresaID == null || c.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<SubCategoriaProducto>().HasQueryFilter(s =>
            _empresaContexto.EmpresaID == null || s.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<CategoriaSubCategoriaRelacion>().HasQueryFilter(r =>
            _empresaContexto.EmpresaID == null || r.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<Combo>().HasQueryFilter(c =>
            _empresaContexto.EmpresaID == null || c.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<ComboLog>().HasQueryFilter(l =>
            _empresaContexto.EmpresaID == null || l.EmpresaID == _empresaContexto.EmpresaID);

        // Licenciamiento con EmpresaID
        modelBuilder.Entity<EmpresaAplicacion>().HasQueryFilter(e =>
            _empresaContexto.EmpresaID == null || e.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<EmpresaPlan>().HasQueryFilter(e =>
            _empresaContexto.EmpresaID == null || e.EmpresaID == _empresaContexto.EmpresaID);

        // Catálogos N:M
        modelBuilder.Entity<ProductoPrecio>().HasQueryFilter(p =>
            _empresaContexto.EmpresaID == null || p.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<ProductoCategoriaComercial>().HasQueryFilter(pc =>
            _empresaContexto.EmpresaID == null || pc.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<ClienteVendedor>().HasQueryFilter(c =>
            _empresaContexto.EmpresaID == null || c.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<ClienteListaPrecios>().HasQueryFilter(c =>
            _empresaContexto.EmpresaID == null || c.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<ClienteDocumento>().HasQueryFilter(c =>
            _empresaContexto.EmpresaID == null || c.EmpresaID == _empresaContexto.EmpresaID);

        // Categorías comerciales
        modelBuilder.Entity<CategoriaComercial>().HasQueryFilter(c =>
            _empresaContexto.EmpresaID == null || c.EmpresaID == _empresaContexto.EmpresaID);

        // Legacy con EmpresaID
        modelBuilder.Entity<Producto>().HasQueryFilter(p =>
            _empresaContexto.EmpresaID == null || p.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<Vendedor>().HasQueryFilter(v =>
            _empresaContexto.EmpresaID == null || v.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<Cliente>().HasQueryFilter(c =>
            _empresaContexto.EmpresaID == null || c.EmpresaID == _empresaContexto.EmpresaID);

        // VendedorListasPrecios — filtro por empresa
        modelBuilder.Entity<VendedorListaPrecios>().HasQueryFilter(v =>
            _empresaContexto.EmpresaID == null || v.EmpresaID == _empresaContexto.EmpresaID);

        // Vendedores — estrellas, coeficientes y estadísticas
        modelBuilder.Entity<VendedorEstrellasDefinicion>().HasQueryFilter(e =>
            _empresaContexto.EmpresaID == null || e.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<VendedorEstrellasCoeficiente>().HasQueryFilter(c =>
            _empresaContexto.EmpresaID == null || c.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<VendedorEstadistica>().HasQueryFilter(e =>
            _empresaContexto.EmpresaID == null || e.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<VendedorEstrellaDiaria>().HasQueryFilter(ed =>
            _empresaContexto.EmpresaID == null || ed.EmpresaID == _empresaContexto.EmpresaID);

        // Pedidos y AltaClientesPendientes gestionados
        modelBuilder.Entity<Pedido>().HasQueryFilter(p =>
            _empresaContexto.EmpresaID == null || p.EmpresaID == _empresaContexto.EmpresaID);
        modelBuilder.Entity<AltaClientePendiente>().HasQueryFilter(a =>
            _empresaContexto.EmpresaID == null || a.EmpresaID == _empresaContexto.EmpresaID);
        // PedidoDetalle: sin Global Query Filter — acceder siempre por PedidosID

        // GpsPosiciones: sin Global Query Filter — filtrar explícitamente en cada query
    }
}
