using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Empresas;

public sealed class CrearEmpresaCommandHandler
{
    private readonly IEmpresaRepository _empresaRepository;

    public CrearEmpresaCommandHandler(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }

    public async Task<int> HandleAsync(CrearEmpresaCommand command, CancellationToken cancellationToken)
    {
        bool nombreOcupado = await _empresaRepository.ExisteNombreAsync(
            command.Nombre, excludeEmpresaID: null, cancellationToken);

        if (nombreOcupado)
            throw new InvalidOperationException($"Ya existe una empresa con el nombre '{command.Nombre}'.");

        Empresa empresa = Empresa.Crear(command.Nombre, command.KeyConexion);
        return await _empresaRepository.CrearAsync(empresa, cancellationToken);
    }
}
