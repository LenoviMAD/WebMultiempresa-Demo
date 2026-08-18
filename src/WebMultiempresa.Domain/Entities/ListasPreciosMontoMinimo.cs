namespace WebMultiempresa.Domain.Entities;

public sealed class ListasPreciosMontoMinimo
{
    public int ListasPreciosMontoMinimoID { get; private set; }
    public int ListasPreciosID { get; private set; }
    public int EmpresaID { get; private set; }
    public decimal MontoMinimo { get; private set; }
    public bool Baja { get; private set; }

    private ListasPreciosMontoMinimo() { }

    public static ListasPreciosMontoMinimo Crear(int listasPreciosId, int empresaId, decimal montoMinimo) =>
        new()
        {
            ListasPreciosID = listasPreciosId,
            EmpresaID       = empresaId,
            MontoMinimo     = montoMinimo,
            Baja            = false
        };

    public void ActualizarMonto(decimal montoMinimo)
    {
        MontoMinimo = montoMinimo;
    }
}
