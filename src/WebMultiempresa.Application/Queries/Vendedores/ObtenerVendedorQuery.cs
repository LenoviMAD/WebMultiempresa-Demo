using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Vendedores;

public sealed class ObtenerVendedorQuery
{
    private readonly IVendedorRepository _repository;

    public ObtenerVendedorQuery(IVendedorRepository repository)
    {
        _repository = repository;
    }

    public async Task<VendedorDto?> HandleAsync(int vendedoresId, CancellationToken cancellationToken)
    {
        Vendedor? vendedor = await _repository.ObtenerPorIdAsync(vendedoresId, cancellationToken);

        return vendedor is null ? null : new VendedorDto(
            vendedor.VendedoresID,
            vendedor.Codigo,
            vendedor.Nombre,
            vendedor.WhatsApp,
            vendedor.CategoriasComercialesID,
            vendedor.VerTodasLasCategorias,
            vendedor.Clave,
            vendedor.DiaInicioRuta,
            vendedor.Baja);
    }
}
