namespace WebMultiempresa.Domain.Entities;

public sealed class ClienteListaPrecios
{
    public int ClienteListasPreciosID { get; private set; }
    public int ClientesID { get; private set; }
    public int ListasPreciosID { get; private set; }
    public int EmpresaID { get; private set; }
    public bool EsPrincipal { get; private set; }
    public bool Baja { get; private set; }

    private ClienteListaPrecios() { }

    public static ClienteListaPrecios Crear(int clientesId, int listasPreciosId, int empresaId, bool esPrincipal = false) =>
        new() { ClientesID = clientesId, ListasPreciosID = listasPreciosId, EmpresaID = empresaId, EsPrincipal = esPrincipal, Baja = false };
}
