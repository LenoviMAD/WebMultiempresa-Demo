using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class MarcarListaDefaultVendedorCommandHandler
{
    private readonly IVendedorListaPreciosRepository _listasRepository;

    public MarcarListaDefaultVendedorCommandHandler(IVendedorListaPreciosRepository listasRepository)
    {
        _listasRepository = listasRepository;
    }

    public async Task HandleAsync(MarcarListaDefaultVendedorCommand command, CancellationToken cancellationToken)
    {
        VendedorListaPrecios? anteriorDefault = await _listasRepository
            .ObtenerDefaultActivaAsync(command.VendedoresID, cancellationToken);

        if (anteriorDefault is not null && anteriorDefault.VendedorListasPreciosID != command.VendedorListasPreciosID)
        {
            anteriorDefault.DesmarcarDefault();
            await _listasRepository.ActualizarAsync(anteriorDefault, cancellationToken);
        }

        VendedorListaPrecios nuevaDefault = await _listasRepository.ObtenerPorIdAsync(
            command.VendedorListasPreciosID, cancellationToken)
            ?? throw new KeyNotFoundException($"Asignación {command.VendedorListasPreciosID} no encontrada.");

        nuevaDefault.MarcarDefault();
        await _listasRepository.ActualizarAsync(nuevaDefault, cancellationToken);
    }
}
