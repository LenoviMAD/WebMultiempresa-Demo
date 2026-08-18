namespace WebMultiempresa.Domain.Entities;

/// <summary>
/// Coeficiente de comisión aplicado según la cantidad de estrellas obtenidas en el día.
/// Ejemplo: 0 estrellas → 0.0, 4 estrellas → 1.0, 6 estrellas → 1.1
/// </summary>
public sealed class VendedorEstrellasCoeficiente
{
    public int VendedorEstrellasCoeficientesID { get; private set; }
    public int EmpresaID { get; private set; }
    public byte CantidadEstrellas { get; private set; }
    public decimal CoeficienteComision { get; private set; }
    public bool Baja { get; private set; }

    private VendedorEstrellasCoeficiente() { }

    public static VendedorEstrellasCoeficiente Crear(
        int empresaId,
        byte cantidadEstrellas,
        decimal coeficienteComision) =>
        new()
        {
            EmpresaID           = empresaId,
            CantidadEstrellas   = cantidadEstrellas,
            CoeficienteComision = coeficienteComision,
            Baja                = false
        };

    public void ActualizarCoeficiente(decimal coeficienteComision)
    {
        CoeficienteComision = coeficienteComision;
    }

    public void DarDeBaja() => Baja = true;
    public void Reactivar() => Baja = false;
}
