namespace WebMultiempresa.Domain.Entities;

public sealed class Cliente
{
    public int ClientesID { get; private set; }
    public int EmpresaID { get; private set; }
    public string Codigo { get; private set; } = string.Empty;
    public string Nombre { get; private set; } = string.Empty;
    public string? Direccion { get; private set; }
    public string? Localidad { get; private set; }
    public string? Provincia { get; private set; }
    public string? CodigoPostal { get; private set; }
    public string? Telefono { get; private set; }
    public string? Email { get; private set; }
    public string? Documento { get; private set; }
    public int? TiposDocumentosID { get; private set; }
    public int DiaDeVenta { get; private set; }
    public int ListaDePrecioID { get; private set; }
    public bool ConsumidorFinal { get; private set; }
    public decimal PorcentajePercepcionIB { get; private set; }
    public decimal? Latitud { get; private set; }
    public decimal? Longitud { get; private set; }
    public string? ClaveHash { get; private set; }
    public bool Baja { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    private Cliente() { }

    public static Cliente Crear(
        int empresaId,
        string codigo,
        string nombre,
        int diaDeVenta,
        int listaDePrecioId,
        bool consumidorFinal,
        string? direccion = null,
        string? localidad = null,
        string? provincia = null,
        string? codigoPostal = null,
        string? telefono = null,
        string? email = null,
        string? documento = null,
        int? tiposDocumentosId = null,
        decimal porcentajePercepcionIB = 0m,
        decimal? latitud = null,
        decimal? longitud = null,
        string? claveHash = null) =>
        new()
        {
            EmpresaID              = empresaId,
            Codigo                 = codigo.Trim(),
            Nombre                 = nombre.Trim(),
            DiaDeVenta             = diaDeVenta,
            ListaDePrecioID        = listaDePrecioId,
            ConsumidorFinal        = consumidorFinal,
            Direccion              = direccion?.Trim(),
            Localidad              = localidad?.Trim(),
            Provincia              = provincia?.Trim(),
            CodigoPostal           = codigoPostal?.Trim(),
            Telefono               = telefono?.Trim(),
            Email                  = email?.Trim()?.ToLowerInvariant(),
            Documento              = documento?.Trim(),
            TiposDocumentosID      = tiposDocumentosId,
            PorcentajePercepcionIB = porcentajePercepcionIB,
            Latitud                = latitud,
            Longitud               = longitud,
            ClaveHash              = claveHash,
            Baja                   = false,
            FechaCreacion          = DateTime.UtcNow
        };

    public void Actualizar(
        string codigo,
        string nombre,
        int diaDeVenta,
        int listaDePrecioId,
        bool consumidorFinal,
        string? direccion,
        string? localidad,
        string? provincia,
        string? codigoPostal,
        string? telefono,
        string? email,
        string? documento,
        int? tiposDocumentosId,
        decimal porcentajePercepcionIB,
        decimal? latitud,
        decimal? longitud)
    {
        Codigo                 = codigo.Trim();
        Nombre                 = nombre.Trim();
        DiaDeVenta             = diaDeVenta;
        ListaDePrecioID        = listaDePrecioId;
        ConsumidorFinal        = consumidorFinal;
        Direccion              = direccion?.Trim();
        Localidad              = localidad?.Trim();
        Provincia              = provincia?.Trim();
        CodigoPostal           = codigoPostal?.Trim();
        Telefono               = telefono?.Trim();
        Email                  = email?.Trim()?.ToLowerInvariant();
        Documento              = documento?.Trim();
        TiposDocumentosID      = tiposDocumentosId;
        PorcentajePercepcionIB = porcentajePercepcionIB;
        Latitud                = latitud;
        Longitud               = longitud;
    }

    public void ActualizarClave(string claveHash) => ClaveHash = claveHash;

    public void DarDeBaja() => Baja = true;

    public void Reactivar() => Baja = false;
}
