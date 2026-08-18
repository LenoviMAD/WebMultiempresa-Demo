using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class AgregarListaVendedorCommandHandler
{
    private readonly IVendedorListaPreciosRepository _listasRepository;
    private readonly IVendedorRepository _vendedorRepository;
    private readonly ICurrentEmpresaContext _empresaContext;

    public AgregarListaVendedorCommandHandler(
        IVendedorListaPreciosRepository listasRepository,
        IVendedorRepository vendedorRepository,
        ICurrentEmpresaContext empresaContext)
    {
        _listasRepository   = listasRepository;
        _vendedorRepository = vendedorRepository;
        _empresaContext     = empresaContext;
    }

    public async Task HandleAsync(AgregarListaVendedorCommand command, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        bool existe = await _listasRepository.ExisteAsignacionActivaAsync(
            command.VendedoresID, command.ListasPreciosID, cancellationToken);

        if (existe)
            throw new InvalidOperationException("La lista de precios ya está asignada a este vendedor.");

        if (command.EsDefault)
        {
            VendedorListaPrecios? defaultActual = await _listasRepository
                .ObtenerDefaultActivaAsync(command.VendedoresID, cancellationToken);

            if (defaultActual is not null)
            {
                defaultActual.DesmarcarDefault();
                await _listasRepository.ActualizarAsync(defaultActual, cancellationToken);
            }
        }

        VendedorListaPrecios nueva = VendedorListaPrecios.Crear(
            command.VendedoresID, command.ListasPreciosID, empresaId, command.EsDefault);

        await _listasRepository.AgregarAsync(nueva, cancellationToken);
    }
}
