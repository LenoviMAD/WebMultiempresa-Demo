using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class AsignarClientesVendedorCommandHandler
{
    private readonly IClienteVendedorRepository _repository;
    private readonly ICurrentEmpresaContext _empresaContext;

    public AsignarClientesVendedorCommandHandler(
        IClienteVendedorRepository repository,
        ICurrentEmpresaContext empresaContext)
    {
        _repository     = repository;
        _empresaContext = empresaContext;
    }

    public async Task HandleAsync(AsignarClientesVendedorCommand command, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        List<ClienteVendedor> nuevas = [];

        foreach (int clienteId in command.ClientesIDs)
        {
            ClienteVendedor? existente = await _repository.ObtenerAsync(
                clienteId, command.VendedoresID, cancellationToken);

            if (existente is not null)
            {
                // Si fue dado de baja, lo reactivamos en lugar de insertar (evita UNIQUE violation)
                if (existente.Baja)
                {
                    existente.Reactivar();
                    await _repository.ActualizarAsync(existente, cancellationToken);
                }
                // Si ya está activo, se ignora
                continue;
            }

            nuevas.Add(ClienteVendedor.Crear(clienteId, command.VendedoresID, empresaId));
        }

        if (nuevas.Count > 0)
            await _repository.AgregarRangoAsync(nuevas, cancellationToken);
    }
}
