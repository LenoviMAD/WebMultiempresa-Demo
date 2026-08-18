using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Exceptions;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Combos;

public sealed class CrearComboCommandHandler
{
    private readonly IComboRepository _comboRepository;
    private readonly ICurrentEmpresaContext _empresaContext;
    private readonly ICurrentUserContext _userContext;

    public CrearComboCommandHandler(
        IComboRepository comboRepository,
        ICurrentEmpresaContext empresaContext,
        ICurrentUserContext userContext)
    {
        _comboRepository = comboRepository;
        _empresaContext = empresaContext;
        _userContext = userContext;
    }

    public async Task<int> HandleAsync(CrearComboCommand command, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        bool codigoOcupado = await _comboRepository.ExisteCodigoAsync(
            empresaId, command.Codigo, excludeCombosID: null, cancellationToken);

        if (codigoOcupado)
            throw new ComboCodigoExistenteException(command.Codigo);

        if (!command.ClientesNumericos && !command.ClientesAlfaNumericos)
            throw new ComboSinTipoClienteException();

        if (!command.TodasLasListasPrecios && command.Items.Count == 0)
            throw new ComboSinListaPrecioException();

        Combo combo = Combo.Crear(
            empresaId: empresaId,
            marcasProductosId: command.MarcasProductosID,
            nombre: command.Nombre,
            codigo: command.Codigo,
            fechaInicio: command.FechaInicio,
            fechaVigencia: command.FechaVigencia,
            nombreAlternativo: command.NombreAlternativo,
            cantidad: command.Cantidad,
            cantidadPorFactura: command.CantidadPorFactura,
            cantidadDinamica: command.CantidadDinamica,
            cantidadSinCargo: command.CantidadSinCargo,
            cantidadXPDV: command.CantidadXPDV,
            montoArrastre: command.MontoArrastre,
            importeProductosFuera: command.ImporteProductosFuera,
            nota: command.Nota,
            tomaProdParaArrastre: command.TomaProdParaArrastre,
            todosLosVendedores: command.TodosLosVendedores,
            todasLasListasPrecios: command.TodasLasListasPrecios,
            todasLasSucursales: command.TodasLasSucursales,
            clientesNumericos: command.ClientesNumericos,
            clientesAlfaNumericos: command.ClientesAlfaNumericos,
            esEstrategico: command.EsEstrategico,
            soloNoCompradores: command.SoloNoCompradores,
            soloNoCompradoresDesde: command.SoloNoCompradoresDesde,
            comboDinamico: command.ComboDinamico,
            esDeIntroduccion: command.EsDeIntroduccion,
            noImprimir: command.NoImprimir,
            impresionResumida: command.ImpresionResumida,
            usarDescuentoClientes: command.UsarDescuentoClientes,
            validarPartido: command.ValidarPartido,
            cupoTotal: command.CupoTotal);

        int combosId = await _comboRepository.CrearAsync(combo, cancellationToken);

        if (command.Items.Count > 0)
        {
            List<ComboItem> items = command.Items
                .Select(i => ComboItem.Crear(
                    combosId: combosId,
                    productosId: i.ProductosID,
                    cantidad: i.Cantidad,
                    precio: i.Precio,
                    tipo: i.Tipo,
                    descuento1: i.Descuento1,
                    descuento2: i.Descuento2,
                    nroGrupoDinamico: i.NroGrupoDinamico))
                .ToList();

            combo.ReemplazarItems(items);
            await _comboRepository.ActualizarAsync(combo, cancellationToken);
        }

        await _comboRepository.ActualizarRelacionesAsync(
            combosId,
            command.VendedoresIDs,
            command.ListasPreciosIDs,
            command.SucursalesIDs,
            cancellationToken);

        await _comboRepository.ActualizarArrastreAsync(
            combosId,
            command.MarcasProductosArrastreIDs,
            command.FamiliaProductosArrastreIDs,
            cancellationToken);

        int usuariosId = _userContext.UsuariosID ?? 0;
        ComboLog log = ComboLog.Crear(combosId, empresaId, usuariosId, _userContext.NombreUsuario, command.Comentario);
        await _comboRepository.AgregarLogAsync(log, cancellationToken);

        return combosId;
    }
}
