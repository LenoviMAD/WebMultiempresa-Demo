namespace WebMultiempresa.Application.Commands.Combos;

public sealed class EditarComboCommand
{
    public int CombosID { get; init; }
    public int? MarcasProductosID { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string? NombreAlternativo { get; init; }
    public string Codigo { get; init; } = string.Empty;
    public DateTime FechaInicio { get; init; }
    public DateTime FechaVigencia { get; init; }
    public int Cantidad { get; init; } = 1;
    public decimal CantidadPorFactura { get; init; } = 1m;
    public decimal CantidadDinamica { get; init; }
    public decimal CantidadSinCargo { get; init; } = 1m;
    public decimal CantidadXPDV { get; init; }
    public decimal MontoArrastre { get; init; }
    public decimal? ImporteProductosFuera { get; init; }
    public string? Nota { get; init; }
    public bool TomaProdParaArrastre { get; init; }
    public bool TodosLosVendedores { get; init; } = true;
    public bool TodasLasListasPrecios { get; init; }
    public bool TodasLasSucursales { get; init; } = true;
    public bool ClientesNumericos { get; init; }
    public bool ClientesAlfaNumericos { get; init; }
    public bool EsEstrategico { get; init; } = true;
    public bool SoloNoCompradores { get; init; }
    public DateTime? SoloNoCompradoresDesde { get; init; }
    public bool ComboDinamico { get; init; }
    public bool EsDeIntroduccion { get; init; }
    public bool NoImprimir { get; init; }
    public bool ImpresionResumida { get; init; }
    public bool UsarDescuentoClientes { get; init; }
    public bool ValidarPartido { get; init; } = true;
    public int? CupoTotal { get; init; }
    public string Comentario { get; init; } = string.Empty;
    public IReadOnlyList<ComboItemInput> Items { get; init; } = [];
    public IReadOnlyList<int> VendedoresIDs { get; init; } = [];
    public IReadOnlyList<int> ListasPreciosIDs { get; init; } = [];
    public IReadOnlyList<int> SucursalesIDs { get; init; } = [];
    public IReadOnlyList<int> MarcasProductosArrastreIDs { get; init; } = [];
    public IReadOnlyList<int> FamiliaProductosArrastreIDs { get; init; } = [];
}
