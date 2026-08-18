namespace WebMultiempresa.Domain.Entities;

public sealed class Sucursal
{
    public int SucursalesID { get; private set; }
    public int EmpresaID { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public bool Baja { get; private set; }

    private Sucursal() { }
}
