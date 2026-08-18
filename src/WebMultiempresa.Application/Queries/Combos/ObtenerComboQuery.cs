using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Combos;

public sealed class ObtenerComboQuery
{
    private readonly IComboRepository _repository;

    public ObtenerComboQuery(IComboRepository repository)
    {
        _repository = repository;
    }

    public async Task<ComboDto?> HandleAsync(int combosId, CancellationToken cancellationToken)
    {
        Combo? combo = await _repository.ObtenerPorIdAsync(combosId, cancellationToken);

        if (combo is null)
            return null;

        List<ComboItemDto> items = combo.Items.Select(i => new ComboItemDto(
            ComboItemsID: i.ComboItemsID,
            ProductosID: i.ProductosID,
            NombreProducto: i.Producto?.Nombre ?? string.Empty,
            CodigoProducto: i.Producto?.Codigo ?? string.Empty,
            Cantidad: i.Cantidad,
            Precio: i.Precio,
            Tipo: i.Tipo,
            Descuento1: i.Descuento1,
            Descuento2: i.Descuento2,
            NroGrupoDinamico: i.NroGrupoDinamico
        )).ToList();

        List<ComboLogDto> logs = combo.Logs
            .OrderByDescending(l => l.Fecha)
            .Select(l => new ComboLogDto(
                ComboLogsID: l.ComboLogsID,
                NombreUsuario: l.NombreUsuario,
                PC: l.PC,
                Comentario: l.Comentario,
                Fecha: l.Fecha
            )).ToList();

        return new ComboDto(
            CombosID: combo.CombosID,
            EmpresaID: combo.EmpresaID,
            MarcasProductosID: combo.MarcasProductosID,
            NombreMarcaProducto: combo.MarcaProducto?.Nombre ?? string.Empty,
            Nombre: combo.Nombre,
            NombreAlternativo: combo.NombreAlternativo,
            Codigo: combo.Codigo,
            Cantidad: combo.Cantidad,
            CantidadFacturada: combo.CantidadFacturada,
            CantidadPorFactura: combo.CantidadPorFactura,
            CantidadDinamica: combo.CantidadDinamica,
            CantidadDinamicaMaxima: combo.CantidadDinamicaMaxima,
            CantidadSinCargo: combo.CantidadSinCargo,
            CantidadXPDV: combo.CantidadXPDV,
            MontoArrastre: combo.MontoArrastre,
            ImporteProductosFuera: combo.ImporteProductosFuera,
            PorcentajeComision: combo.PorcentajeComision,
            Nota: combo.Nota,
            FechaInicio: combo.FechaInicio,
            FechaVigencia: combo.FechaVigencia,
            TomaProdParaArrastre: combo.TomaProdParaArrastre,
            TodosLosVendedores: combo.TodosLosVendedores,
            TodasLasListasPrecios: combo.TodasLasListasPrecios,
            TodasLasSucursales: combo.TodasLasSucursales,
            ClientesNumericos: combo.ClientesNumericos,
            ClientesAlfaNumericos: combo.ClientesAlfaNumericos,
            EsEstrategico: combo.EsEstrategico,
            SoloNoCompradores: combo.SoloNoCompradores,
            SoloNoCompradoresDesde: combo.SoloNoCompradoresDesde,
            ComboDinamico: combo.ComboDinamico,
            EsDeIntroduccion: combo.EsDeIntroduccion,
            NoImprimir: combo.NoImprimir,
            ImpresionResumida: combo.ImpresionResumida,
            UsarDescuentoClientes: combo.UsarDescuentoClientes,
            ValidarPartido: combo.ValidarPartido,
            CupoTotal: combo.CupoTotal,
            Baja: combo.Baja,
            Items: items,
            VendedoresIDs: combo.Vendedores.Select(v => v.VendedoresID).ToArray(),
            ListasPreciosIDs: combo.ListasPrecios.Select(l => l.ListasPreciosID).ToArray(),
            SucursalesIDs: combo.Sucursales.Select(s => s.SucursalesID).ToArray(),
            MarcasProductosArrastreIDs: combo.MarcasProductosArrastre.Select(r => r.MarcasProductosID).ToArray(),
            FamiliaProductosArrastreIDs: combo.FamiliaProductosArrastre.Select(m => m.FamiliaProductosID).ToArray(),
            Logs: logs
        );
    }
}
