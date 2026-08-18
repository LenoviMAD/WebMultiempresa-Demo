namespace WebMultiempresa.Domain.Interfaces;

public interface IClienteRepository
{
    Task<IReadOnlyList<Domain.Entities.Cliente>> ListarActivosAsync(CancellationToken cancellationToken);
}
