using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Exceptions;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class EditarVendedorCommandHandler
{
    private readonly IVendedorRepository _repository;
    private readonly ICurrentEmpresaContext _empresaContext;

    public EditarVendedorCommandHandler(
        IVendedorRepository repository,
        ICurrentEmpresaContext empresaContext)
    {
        _repository     = repository;
        _empresaContext = empresaContext;
    }

    public async Task HandleAsync(EditarVendedorCommand command, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        Vendedor vendedor = await _repository.ObtenerPorIdAsync(command.VendedoresID, cancellationToken)
            ?? throw new KeyNotFoundException($"Vendedor {command.VendedoresID} no encontrado.");

        bool codigoOcupado = await _repository.ExisteCodigoAsync(
            command.Codigo, empresaId, excludeVendedoresId: command.VendedoresID, cancellationToken);

        if (codigoOcupado)
            throw new VendedorCodigoExistenteException(command.Codigo);

        string? clave = string.IsNullOrWhiteSpace(command.Clave) ? null : command.Clave;

        vendedor.Actualizar(
            command.Codigo,
            command.Nombre,
            command.WhatsApp,
            command.TiposDeVendedoresID,
            command.CategoriasComercialesID,
            command.DiaInicioRuta,
            command.VerTodasLasCategorias,
            clave);

        await _repository.ActualizarAsync(vendedor, cancellationToken);
    }
}
