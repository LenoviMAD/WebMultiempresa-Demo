namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class GuardarEstrellasCoeficienteCommand
{
    public int? VendedorEstrellasCoeficientesID { get; init; }
    public byte CantidadEstrellas { get; init; }
    public decimal CoeficienteComision { get; init; }
}
