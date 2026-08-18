using WebMultiempresa.Domain.Exceptions;

namespace WebMultiempresa.Domain.Entities;

public sealed class Vendedor
{
    public int VendedoresID { get; private set; }
    public int EmpresaID { get; private set; }
    public int? TiposDeVendedoresID { get; private set; }
    public int? CategoriasComercialesID { get; private set; }
    public string Codigo { get; private set; } = string.Empty;
    public string Nombre { get; private set; } = string.Empty;
    public string? WhatsApp { get; private set; }
    public string? Clave { get; private set; }
    /// <summary>Día de la semana en que inicia la ruta: 0=lunes … 6=domingo.</summary>
    public int DiaInicioRuta { get; private set; }
    public bool VerTodasLasCategorias { get; private set; }
    public bool Baja { get; private set; }

    private Vendedor() { }

    public static Vendedor Crear(
        int empresaId,
        string codigo,
        string nombre,
        string? whatsApp = null,
        string? clave = null,
        int? tiposDeVendedoresId = null,
        int? categoriasComercialesId = null,
        int diaInicioRuta = 0,
        bool verTodasLasCategorias = false) =>
        new()
        {
            EmpresaID               = empresaId,
            Codigo                  = codigo.Trim().ToUpperInvariant(),
            Nombre                  = nombre.Trim(),
            WhatsApp                = whatsApp?.Trim(),
            Clave                   = clave,
            TiposDeVendedoresID     = tiposDeVendedoresId,
            CategoriasComercialesID = categoriasComercialesId,
            DiaInicioRuta           = diaInicioRuta,
            VerTodasLasCategorias   = verTodasLasCategorias,
            Baja                    = false
        };

    public void Actualizar(
        string codigo,
        string nombre,
        string? whatsApp,
        int? tiposDeVendedoresId,
        int? categoriasComercialesId,
        int diaInicioRuta,
        bool verTodasLasCategorias,
        string? clave = null)
    {
        Codigo                  = codigo.Trim().ToUpperInvariant();
        Nombre                  = nombre.Trim();
        WhatsApp                = whatsApp?.Trim();
        TiposDeVendedoresID     = tiposDeVendedoresId;
        CategoriasComercialesID = categoriasComercialesId;
        DiaInicioRuta           = diaInicioRuta;
        VerTodasLasCategorias   = verTodasLasCategorias;
        if (clave is not null)
            Clave = clave;
    }

    public void DarDeBaja()
    {
        if (Baja) throw new VendedorYaDadoDeBajaException(VendedoresID);
        Baja = true;
    }

    public void Reactivar()
    {
        if (!Baja) throw new VendedorYaActivoException(VendedoresID);
        Baja = false;
    }
}
