using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Empresas;

public sealed class BajaEmpresaCommandHandler
{
    private readonly IEmpresaRepository _empresaRepository;

    public BajaEmpresaCommandHandler(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }

    public async Task HandleAsync(int empresaId, CancellationToken cancellationToken)
    {
        await _empresaRepository.BajaLogicaAsync(empresaId, cancellationToken);
    }
}
