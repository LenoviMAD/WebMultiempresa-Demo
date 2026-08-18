using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Exceptions;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class QuitarListaVendedorCommandHandler
{
    private readonly IVendedorListaPreciosRepository _listasRepository;

    public QuitarListaVendedorCommandHandler(IVendedorListaPreciosRepository listasRepository)
    {
        _listasRepository = listasRepository;
    }

    public async Task HandleAsync(QuitarListaVendedorCommand command, CancellationToken cancellationToken)
    {
        VendedorListaPrecios lista = await _listasRepository.ObtenerPorIdAsync(
            command.VendedorListasPreciosID, cancellationToken)
            ?? throw new KeyNotFoundException($"Asignación {command.VendedorListasPreciosID} no encontrada.");

        if (lista.EsDefault)
            throw new VendedorListaDefaultRequeridaException();

        lista.DarDeBaja();
        await _listasRepository.ActualizarAsync(lista, cancellationToken);
    }
}
