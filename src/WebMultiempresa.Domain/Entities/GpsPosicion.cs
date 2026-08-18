namespace WebMultiempresa.Domain.Entities;

public sealed class GpsPosicion
{
    public long GpsPosicionesID { get; private set; }
    public int EmpresaID { get; private set; }
    public int AplicacionesID { get; private set; }
    public int VendedoresID { get; private set; }
    public decimal Latitud { get; private set; }
    public decimal Longitud { get; private set; }
    public decimal? VelocidadKmh { get; private set; }
    public int? AccuracyMetros { get; private set; }
    public decimal? DistanciaMetros { get; private set; }
    public string? ModeloDispositivo { get; private set; }
    public string? NumeroDispositivo { get; private set; }
    public string? OrigenEnvio { get; private set; }
    public DateTime FechaHora { get; private set; }

    private GpsPosicion() { }

    public static GpsPosicion Crear(
        int empresaId,
        int aplicacionesId,
        int vendedoresId,
        decimal latitud,
        decimal longitud,
        decimal? velocidadKmh = null,
        int? accuracyMetros = null,
        decimal? distanciaMetros = null,
        string? modeloDispositivo = null,
        string? numeroDispositivo = null,
        string? origenEnvio = null) =>
        new()
        {
            EmpresaID         = empresaId,
            AplicacionesID    = aplicacionesId,
            VendedoresID      = vendedoresId,
            Latitud           = latitud,
            Longitud          = longitud,
            VelocidadKmh      = velocidadKmh,
            AccuracyMetros    = accuracyMetros,
            DistanciaMetros   = distanciaMetros,
            ModeloDispositivo = modeloDispositivo,
            NumeroDispositivo = numeroDispositivo,
            OrigenEnvio       = origenEnvio,
            FechaHora         = DateTime.UtcNow
        };
}
