namespace WebMultiempresa.Domain.Entities;

public sealed class ListaPrecios
{
    public int ListasPreciosID { get; private set; }
    public int EmpresaID { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public decimal PorcentajeMarcup { get; private set; }
    public bool Baja { get; private set; }

    private ListaPrecios() { }

    public static ListaPrecios Crear(int empresaId, string nombre, decimal porcentajeMarcup = 0m) =>
        new()
        {
            EmpresaID        = empresaId,
            Nombre           = nombre.Trim(),
            PorcentajeMarcup = porcentajeMarcup,
            Baja             = false
        };

    public void Actualizar(string nombre, decimal porcentajeMarcup)
    {
        Nombre           = nombre.Trim();
        PorcentajeMarcup = porcentajeMarcup;
    }

    public void DarDeBaja() => Baja = true;
}
