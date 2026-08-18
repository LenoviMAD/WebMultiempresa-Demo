namespace WebMultiempresa.Domain.Entities;

public sealed class VendedorListaPrecios
{
    public int VendedorListasPreciosID { get; private set; }
    public int VendedoresID { get; private set; }
    public int ListasPreciosID { get; private set; }
    public bool EsDefault { get; private set; }
    public int EmpresaID { get; private set; }
    public bool Baja { get; private set; }

    private VendedorListaPrecios() { }

    public static VendedorListaPrecios Crear(
        int vendedoresId,
        int listaPreciosId,
        int empresaId,
        bool esDefault) =>
        new()
        {
            VendedoresID    = vendedoresId,
            ListasPreciosID = listaPreciosId,
            EmpresaID       = empresaId,
            EsDefault       = esDefault,
            Baja            = false
        };

    public void MarcarDefault() => EsDefault = true;
    public void DesmarcarDefault() => EsDefault = false;
    public void DarDeBaja() => Baja = true;
}
