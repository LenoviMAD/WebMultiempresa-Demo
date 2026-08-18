namespace WebMultiempresa.Domain.Entities;

public sealed class Producto
{
    public int ProductosID { get; private set; }
    public int EmpresaID { get; private set; }
    public string Codigo { get; private set; } = string.Empty;
    public int? ProductosIDPadre { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public int? MarcasProductosID { get; private set; }
    public int? FamiliaProductosID { get; private set; }
    public int CantidadMultiplo { get; private set; } = 1;
    public int UnidadesPorBulto { get; private set; } = 1;
    public int StockEnUnidades { get; private set; }
    public bool StockPropio { get; private set; } = true;
    public int? ImpuestosID { get; private set; }
    public string? UrlImagenWeb { get; private set; }
    public decimal? PrecioCosto { get; private set; }
    public bool DesactivadoParaLaVenta { get; private set; }
    public bool Baja { get; private set; }
    public DateTime FechaAlta { get; private set; }
    public DateTime FechaActualizacion { get; private set; }
    public DateTime? FechaUltimoReingreso { get; private set; }

    private Producto() { }

    public static Producto Crear(
        int empresaId,
        string codigo,
        string nombre,
        int? productosIdPadre = null,
        int? marcasProductosId = null,
        int? familiaProductosId = null,
        int cantidadMultiplo = 1,
        int unidadesPorBulto = 1,
        bool stockPropio = true,
        int? impuestosId = null,
        string? urlImagenWeb = null) =>
        new()
        {
            EmpresaID                = empresaId,
            Codigo                   = codigo.Trim().ToUpperInvariant(),
            Nombre                   = nombre.Trim(),
            ProductosIDPadre         = productosIdPadre,
            MarcasProductosID        = marcasProductosId,
            FamiliaProductosID       = familiaProductosId,
            CantidadMultiplo         = cantidadMultiplo,
            UnidadesPorBulto         = unidadesPorBulto,
            StockEnUnidades          = 0,
            StockPropio              = stockPropio,
            ImpuestosID              = impuestosId,
            UrlImagenWeb             = urlImagenWeb?.Trim(),
            DesactivadoParaLaVenta   = false,
            Baja                     = false,
            FechaAlta                = DateTime.UtcNow,
            FechaActualizacion       = DateTime.UtcNow
        };

    public void Actualizar(
        string codigo,
        string nombre,
        int? productosIdPadre,
        int? marcasProductosId,
        int? familiaProductosId,
        int cantidadMultiplo,
        int unidadesPorBulto,
        bool stockPropio,
        int? impuestosId,
        string? urlImagenWeb)
    {
        Codigo                   = codigo.Trim().ToUpperInvariant();
        Nombre                   = nombre.Trim();
        ProductosIDPadre         = productosIdPadre;
        MarcasProductosID        = marcasProductosId;
        FamiliaProductosID       = familiaProductosId;
        CantidadMultiplo         = cantidadMultiplo;
        UnidadesPorBulto         = unidadesPorBulto;
        StockPropio              = stockPropio;
        ImpuestosID              = impuestosId;
        UrlImagenWeb             = urlImagenWeb?.Trim();
        FechaActualizacion       = DateTime.UtcNow;
    }

    public void ActualizarStock(int stock)
    {
        if (StockEnUnidades < 1 && stock > 0)
            FechaUltimoReingreso = DateTime.UtcNow;
        StockEnUnidades    = stock;
        FechaActualizacion = DateTime.UtcNow;
    }

    public void ActivarParaLaVenta()
    {
        DesactivadoParaLaVenta = false;
        FechaActualizacion     = DateTime.UtcNow;
    }

    public void DesactivarParaLaVenta()
    {
        DesactivadoParaLaVenta = true;
        FechaActualizacion     = DateTime.UtcNow;
    }

    public void ActualizarPrecioCosto(decimal? precioCosto)
    {
        PrecioCosto        = precioCosto;
        FechaActualizacion = DateTime.UtcNow;
    }

    public void DarDeBaja() => Baja = true;
}
