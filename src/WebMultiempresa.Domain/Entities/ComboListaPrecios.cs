namespace WebMultiempresa.Domain.Entities;

public sealed class ComboListaPrecios
{
    public int ComboListaPreciosID { get; private set; }
    public int CombosID { get; private set; }
    public int ListasPreciosID { get; private set; }

    private ComboListaPrecios() { }

    public static ComboListaPrecios Crear(int combosId, int listasPreciosId) =>
        new() { CombosID = combosId, ListasPreciosID = listasPreciosId };
}
