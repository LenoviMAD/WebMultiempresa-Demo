namespace WebMultiempresa.Domain.Entities;

public sealed class VendedorEstadistica
{
    public long VendedorEstadisticasID { get; private set; }
    public int VendedoresID { get; private set; }
    public int EmpresaID { get; private set; }
    public DateTime Fecha { get; private set; }
    /// <summary>Snapshot del coeficiente de comisión vigente ese día, derivado de las estrellas obtenidas.</summary>
    public decimal CoeficienteComision { get; private set; }
    public bool Baja { get; private set; }

    private VendedorEstadistica() { }

    public static VendedorEstadistica Crear(
        int vendedoresId,
        int empresaId,
        DateTime fecha,
        decimal coeficienteComision) =>
        new()
        {
            VendedoresID        = vendedoresId,
            EmpresaID           = empresaId,
            Fecha               = fecha.Date,
            CoeficienteComision = coeficienteComision,
            Baja                = false
        };

    public void ActualizarCoeficiente(decimal coeficienteComision)
        => CoeficienteComision = coeficienteComision;

    public void DarDeBaja() => Baja = true;
}
