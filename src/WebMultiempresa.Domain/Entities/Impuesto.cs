namespace WebMultiempresa.Domain.Entities;

public sealed class Impuesto
{
    public int ImpuestosID { get; private set; }
    public int EmpresaID { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public decimal Porcentaje { get; private set; }
    public bool Baja { get; private set; }

    private Impuesto() { }

    public static Impuesto Crear(int empresaId, string nombre, decimal porcentaje) =>
        new()
        {
            EmpresaID  = empresaId,
            Nombre     = nombre.Trim(),
            Porcentaje = porcentaje,
            Baja       = false
        };

    public void Actualizar(string nombre, decimal porcentaje)
    {
        Nombre     = nombre.Trim();
        Porcentaje = porcentaje;
    }

    public void DarDeBaja() => Baja = true;
}
