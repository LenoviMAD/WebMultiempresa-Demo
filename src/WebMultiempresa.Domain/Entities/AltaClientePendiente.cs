namespace WebMultiempresa.Domain.Entities;

public sealed class AltaClientePendiente
{
    public long AltaClientesPendientesID { get; private set; }
    public int EmpresaID { get; private set; }
    public byte TipoUsuario { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string? Documento { get; private set; }
    public byte? TipoDocumento { get; private set; }
    public string? Email { get; private set; }
    public string? Telefono { get; private set; }
    public string? TelefonoNormalizado { get; private set; }
    public string? Localidad { get; private set; }
    public string? Provincia { get; private set; }
    public string? DireccionEnvio { get; private set; }
    public string? CodigoPostal { get; private set; }
    public decimal? Latitud { get; private set; }
    public decimal? Longitud { get; private set; }
    public string? Password { get; private set; }
    public byte Estado { get; private set; }
    public int? ClientesID { get; private set; }
    public string? ErrorDetalle { get; private set; }
    public DateTime FechaSolicitud { get; private set; }
    public DateTime? FechaConfirmacion { get; private set; }
    public DateTime? FechaProcesamiento { get; private set; }

    public const byte EstadoPendiente  = 0;
    public const byte EstadoConfirmado = 1;
    public const byte EstadoRechazado  = 2;
    public const byte EstadoProcesado  = 3;
    public const byte EstadoError      = 4;

    private AltaClientePendiente() { }

    public static AltaClientePendiente Crear(
        int empresaId,
        byte tipoUsuario,
        string nombre,
        string? documento,
        byte? tipoDocumento,
        string? email,
        string? telefono,
        string? telefonoNormalizado,
        string? localidad,
        string? provincia,
        string? direccionEnvio,
        string? codigoPostal,
        decimal? latitud,
        decimal? longitud,
        string? password) =>
        new()
        {
            EmpresaID           = empresaId,
            TipoUsuario         = tipoUsuario,
            Nombre              = nombre.Trim(),
            Documento           = documento?.Trim(),
            TipoDocumento       = tipoDocumento,
            Email               = email?.Trim()?.ToLowerInvariant(),
            Telefono            = telefono?.Trim(),
            TelefonoNormalizado = telefonoNormalizado?.Trim(),
            Localidad           = localidad?.Trim(),
            Provincia           = provincia?.Trim(),
            DireccionEnvio      = direccionEnvio?.Trim(),
            CodigoPostal        = codigoPostal?.Trim(),
            Latitud             = latitud,
            Longitud            = longitud,
            Password            = password,
            Estado              = EstadoPendiente,
            FechaSolicitud      = DateTime.UtcNow
        };

    public void MarcarConfirmado()
    {
        Estado            = EstadoConfirmado;
        FechaConfirmacion = DateTime.UtcNow;
    }

    public void MarcarRechazado()
    {
        Estado            = EstadoRechazado;
        FechaConfirmacion = DateTime.UtcNow;
    }

    public void MarcarProcesado(int clientesId)
    {
        Estado             = EstadoProcesado;
        ClientesID         = clientesId;
        FechaProcesamiento = DateTime.UtcNow;
    }

    public void GuardarError(string errorDetalle)
    {
        Estado             = EstadoError;
        ErrorDetalle       = errorDetalle;
        FechaProcesamiento = DateTime.UtcNow;
    }
}
