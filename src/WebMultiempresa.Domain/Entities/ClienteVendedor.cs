namespace WebMultiempresa.Domain.Entities;

public sealed class ClienteVendedor
{
    public int ClienteVendedoresID { get; private set; }
    public int ClientesID { get; private set; }
    public int VendedoresID { get; private set; }
    public int EmpresaID { get; private set; }
    public bool Baja { get; private set; }

    private ClienteVendedor() { }

    public static ClienteVendedor Crear(int clientesId, int vendedoresId, int empresaId) =>
        new() { ClientesID = clientesId, VendedoresID = vendedoresId, EmpresaID = empresaId, Baja = false };

    public void DarDeBaja() => Baja = true;
    public void Reactivar()  => Baja = false;
}
