namespace WebMultiempresa.Domain.Entities;

public sealed class VendedorEstrellasDefinicion
{
    public int VendedorEstrellasDefinicionesID { get; private set; }
    public int EmpresaID { get; private set; }
    public byte NumeroEstrella { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public decimal? ObjetivoMensual { get; private set; }
    public bool Baja { get; private set; }

    private VendedorEstrellasDefinicion() { }

    public static VendedorEstrellasDefinicion Crear(
        int empresaId,
        byte numeroEstrella,
        string nombre,
        decimal? objetivoMensual = null) =>
        new()
        {
            EmpresaID       = empresaId,
            NumeroEstrella  = numeroEstrella,
            Nombre          = nombre.Trim(),
            ObjetivoMensual = objetivoMensual,
            Baja            = false
        };

    public void Actualizar(string nombre, decimal? objetivoMensual)
    {
        Nombre          = nombre.Trim();
        ObjetivoMensual = objetivoMensual;
    }

    public void DarDeBaja() => Baja = true;
    public void Reactivar() => Baja = false;
}
