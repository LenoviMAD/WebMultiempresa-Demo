namespace WebMultiempresa.Domain.Entities;

public sealed class Combo
{
    public int CombosID { get; private set; }
    public int EmpresaID { get; private set; }
    public int? MarcasProductosID { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string? NombreAlternativo { get; private set; }
    public string Codigo { get; private set; } = string.Empty;
    public int Cantidad { get; private set; }
    public int CantidadFacturada { get; private set; }
    public decimal CantidadPorFactura { get; private set; }
    public decimal CantidadDinamica { get; private set; }
    public int CantidadDinamicaMaxima { get; private set; }
    public decimal CantidadSinCargo { get; private set; }
    public decimal CantidadXPDV { get; private set; }
    public decimal MontoArrastre { get; private set; }
    public decimal? ImporteProductosFuera { get; private set; }
    public decimal PorcentajeComision { get; private set; }
    public string? Nota { get; private set; }
    public DateTime FechaInicio { get; private set; }
    public DateTime FechaVigencia { get; private set; }
    public bool TomaProdParaArrastre { get; private set; }
    public bool TodosLosVendedores { get; private set; }
    public bool TodasLasListasPrecios { get; private set; }
    public bool TodasLasSucursales { get; private set; }
    public bool ClientesNumericos { get; private set; }
    public bool ClientesAlfaNumericos { get; private set; }
    public bool EsEstrategico { get; private set; }
    public bool SoloNoCompradores { get; private set; }
    public DateTime? SoloNoCompradoresDesde { get; private set; }
    public bool ComboDinamico { get; private set; }
    public bool EsDeIntroduccion { get; private set; }
    public bool NoImprimir { get; private set; }
    public bool ImpresionResumida { get; private set; }
    public bool UsarDescuentoClientes { get; private set; }
    public bool ValidarPartido { get; private set; }
    public int? CupoTotal { get; private set; }
    public bool Baja { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    public MarcaProducto? MarcaProducto { get; private set; }

    private readonly List<ComboItem> _items = new();
    public IReadOnlyCollection<ComboItem> Items => _items.AsReadOnly();

    private readonly List<ComboFecha> _fechas = new();
    public IReadOnlyCollection<ComboFecha> Fechas => _fechas.AsReadOnly();

    private readonly List<ComboVendedor> _vendedores = new();
    public IReadOnlyCollection<ComboVendedor> Vendedores => _vendedores.AsReadOnly();

    private readonly List<ComboListaPrecios> _listasPrecios = new();
    public IReadOnlyCollection<ComboListaPrecios> ListasPrecios => _listasPrecios.AsReadOnly();

    private readonly List<ComboSucursal> _sucursales = new();
    public IReadOnlyCollection<ComboSucursal> Sucursales => _sucursales.AsReadOnly();

    private readonly List<ComboMarcaProducto> _marcasProductosArrastre = new();
    public IReadOnlyCollection<ComboMarcaProducto> MarcasProductosArrastre => _marcasProductosArrastre.AsReadOnly();

    private readonly List<ComboFamiliaProducto> _familiaProductosArrastre = new();
    public IReadOnlyCollection<ComboFamiliaProducto> FamiliaProductosArrastre => _familiaProductosArrastre.AsReadOnly();

    private readonly List<ComboLog> _logs = new();
    public IReadOnlyCollection<ComboLog> Logs => _logs.AsReadOnly();

    private Combo() { }

    public static Combo Crear(
        int empresaId,
        int? marcasProductosId,
        string nombre,
        string codigo,
        DateTime fechaInicio,
        DateTime fechaVigencia,
        string? nombreAlternativo = null,
        int cantidad = 1,
        decimal cantidadPorFactura = 1m,
        decimal cantidadDinamica = 0m,
        decimal cantidadSinCargo = 1m,
        decimal cantidadXPDV = 0m,
        decimal montoArrastre = 0m,
        decimal? importeProductosFuera = null,
        string? nota = null,
        bool tomaProdParaArrastre = false,
        bool todosLosVendedores = true,
        bool todasLasListasPrecios = false,
        bool todasLasSucursales = true,
        bool clientesNumericos = false,
        bool clientesAlfaNumericos = false,
        bool esEstrategico = true,
        bool soloNoCompradores = false,
        DateTime? soloNoCompradoresDesde = null,
        bool comboDinamico = false,
        bool esDeIntroduccion = false,
        bool noImprimir = false,
        bool impresionResumida = false,
        bool usarDescuentoClientes = false,
        bool validarPartido = true,
        int? cupoTotal = null)
    {
        return new Combo
        {
            EmpresaID = empresaId,
            MarcasProductosID = marcasProductosId,
            Nombre = nombre,
            Codigo = codigo,
            NombreAlternativo = nombreAlternativo,
            FechaInicio = fechaInicio,
            FechaVigencia = fechaVigencia,
            Cantidad = cantidad,
            CantidadPorFactura = cantidadPorFactura,
            CantidadDinamica = cantidadDinamica,
            CantidadSinCargo = cantidadSinCargo,
            CantidadXPDV = cantidadXPDV,
            MontoArrastre = montoArrastre,
            ImporteProductosFuera = importeProductosFuera,
            Nota = nota,
            TomaProdParaArrastre = tomaProdParaArrastre,
            TodosLosVendedores = todosLosVendedores,
            TodasLasListasPrecios = todasLasListasPrecios,
            TodasLasSucursales = todasLasSucursales,
            ClientesNumericos = clientesNumericos,
            ClientesAlfaNumericos = clientesAlfaNumericos,
            EsEstrategico = esEstrategico,
            SoloNoCompradores = soloNoCompradores,
            SoloNoCompradoresDesde = soloNoCompradoresDesde,
            ComboDinamico = comboDinamico,
            EsDeIntroduccion = esDeIntroduccion,
            NoImprimir = noImprimir,
            ImpresionResumida = impresionResumida,
            UsarDescuentoClientes = usarDescuentoClientes,
            ValidarPartido = validarPartido,
            CupoTotal = cupoTotal,
            Baja = false,
            FechaCreacion = DateTime.UtcNow
        };
    }

    public void Actualizar(
        int? marcasProductosId,
        string nombre,
        string codigo,
        DateTime fechaInicio,
        DateTime fechaVigencia,
        string? nombreAlternativo = null,
        int cantidad = 1,
        decimal cantidadPorFactura = 1m,
        decimal cantidadDinamica = 0m,
        decimal cantidadSinCargo = 1m,
        decimal cantidadXPDV = 0m,
        decimal montoArrastre = 0m,
        decimal? importeProductosFuera = null,
        string? nota = null,
        bool tomaProdParaArrastre = false,
        bool todosLosVendedores = true,
        bool todasLasListasPrecios = false,
        bool todasLasSucursales = true,
        bool clientesNumericos = false,
        bool clientesAlfaNumericos = false,
        bool esEstrategico = true,
        bool soloNoCompradores = false,
        DateTime? soloNoCompradoresDesde = null,
        bool comboDinamico = false,
        bool esDeIntroduccion = false,
        bool noImprimir = false,
        bool impresionResumida = false,
        bool usarDescuentoClientes = false,
        bool validarPartido = true,
        int? cupoTotal = null)
    {
        MarcasProductosID = marcasProductosId;
        Nombre = nombre;
        Codigo = codigo;
        NombreAlternativo = nombreAlternativo;
        FechaInicio = fechaInicio;
        FechaVigencia = fechaVigencia;
        Cantidad = cantidad;
        CantidadPorFactura = cantidadPorFactura;
        CantidadDinamica = cantidadDinamica;
        CantidadSinCargo = cantidadSinCargo;
        CantidadXPDV = cantidadXPDV;
        MontoArrastre = montoArrastre;
        ImporteProductosFuera = importeProductosFuera;
        Nota = nota;
        TomaProdParaArrastre = tomaProdParaArrastre;
        TodosLosVendedores = todosLosVendedores;
        TodasLasListasPrecios = todasLasListasPrecios;
        TodasLasSucursales = todasLasSucursales;
        ClientesNumericos = clientesNumericos;
        ClientesAlfaNumericos = clientesAlfaNumericos;
        EsEstrategico = esEstrategico;
        SoloNoCompradores = soloNoCompradores;
        SoloNoCompradoresDesde = soloNoCompradoresDesde;
        ComboDinamico = comboDinamico;
        EsDeIntroduccion = esDeIntroduccion;
        NoImprimir = noImprimir;
        ImpresionResumida = impresionResumida;
        UsarDescuentoClientes = usarDescuentoClientes;
        ValidarPartido = validarPartido;
        CupoTotal = cupoTotal;
    }

    public void ReemplazarItems(IEnumerable<ComboItem> items)
    {
        _items.Clear();
        _items.AddRange(items);
    }
}
