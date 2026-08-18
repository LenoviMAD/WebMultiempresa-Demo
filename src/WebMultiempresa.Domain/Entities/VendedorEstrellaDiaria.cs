namespace WebMultiempresa.Domain.Entities;

public sealed class VendedorEstrellaDiaria
{
    public long VendedorEstrellaDiariasID { get; private set; }
    public int VendedoresID { get; private set; }
    public int EmpresaID { get; private set; }
    public DateTime Fecha { get; private set; }
    public int VendedorEstrellasDefinicionesID { get; private set; }
    public bool EstaEncendida { get; private set; }
    /// <summary>Valor medido ese día para esta estrella (ej: horas en ruta, % cobertura, etc.).</summary>
    public decimal Valor { get; private set; }
    public bool Baja { get; private set; }

    private VendedorEstrellaDiaria() { }

    public static VendedorEstrellaDiaria Crear(
        int vendedoresId,
        int empresaId,
        DateTime fecha,
        int vendedorEstrellasDefinicionesId,
        bool estaEncendida,
        decimal valor) =>
        new()
        {
            VendedoresID                    = vendedoresId,
            EmpresaID                       = empresaId,
            Fecha                           = fecha.Date,
            VendedorEstrellasDefinicionesID = vendedorEstrellasDefinicionesId,
            EstaEncendida                   = estaEncendida,
            Valor                           = valor,
            Baja                            = false
        };
}
