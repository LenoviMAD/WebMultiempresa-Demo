using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Exceptions;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Combos;

public sealed class EditarComboCommandHandler
{
    private readonly IComboRepository _comboRepository;
    private readonly ICurrentEmpresaContext _empresaContext;
    private readonly ICurrentUserContext _userContext;

    public EditarComboCommandHandler(
        IComboRepository comboRepository,
        ICurrentEmpresaContext empresaContext,
        ICurrentUserContext userContext)
    {
        _comboRepository = comboRepository;
        _empresaContext = empresaContext;
        _userContext = userContext;
    }

    public async Task HandleAsync(EditarComboCommand command, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        Combo combo = await _comboRepository.ObtenerPorIdAsync(command.CombosID, cancellationToken)
            ?? throw new KeyNotFoundException($"Combo {command.CombosID} no encontrado.");

        bool codigoOcupado = await _comboRepository.ExisteCodigoAsync(
            empresaId, command.Codigo, excludeCombosID: command.CombosID, cancellationToken);

        if (codigoOcupado)
            throw new ComboCodigoExistenteException(command.Codigo);

        if (!command.ClientesNumericos && !command.ClientesAlfaNumericos)
            throw new ComboSinTipoClienteException();

        combo.Actualizar(
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

        List<ComboItem> items = command.Items
            .Select(i => ComboItem.Crear(
                combosId: command.CombosID,
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

        await _comboRepository.ActualizarRelacionesAsync(
            command.CombosID,
            command.VendedoresIDs,
            command.ListasPreciosIDs,
            command.SucursalesIDs,
            cancellationToken);

        await _comboRepository.ActualizarArrastreAsync(
            command.CombosID,
            command.MarcasProductosArrastreIDs,
            command.FamiliaProductosArrastreIDs,
            cancellationToken);

        int usuariosId = _userContext.UsuariosID ?? 0;
        string comentario = string.IsNullOrWhiteSpace(command.Comentario) ? "Modificación" : command.Comentario;
        ComboLog log = ComboLog.Crear(command.CombosID, empresaId, usuariosId, _userContext.NombreUsuario, comentario);
        await _comboRepository.AgregarLogAsync(log, cancellationToken);
    }
}
