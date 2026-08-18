using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Empresas;

public sealed class EditarEmpresaCommandHandler
{
    private readonly IEmpresaRepository _empresaRepository;

    public EditarEmpresaCommandHandler(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }

    public async Task HandleAsync(EditarEmpresaCommand command, CancellationToken cancellationToken)
    {
        Empresa empresa = await _empresaRepository.ObtenerPorIdAsync(command.EmpresaID, cancellationToken)
            ?? throw new KeyNotFoundException($"Empresa {command.EmpresaID} no encontrada.");

        bool nombreOcupado = await _empresaRepository.ExisteNombreAsync(
            command.Nombre, excludeEmpresaID: command.EmpresaID, cancellationToken);

        if (nombreOcupado)
            throw new InvalidOperationException($"Ya existe una empresa con el nombre '{command.Nombre}'.");

        empresa.Actualizar(command.Nombre, command.KeyConexion);
        await _empresaRepository.ActualizarAsync(empresa, cancellationToken);
    }
}
