using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Services;

public sealed class ComboReadService : IComboReadPort
{
    private readonly AppDbContext _dbContext;

    public ComboReadService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ComboListadoDto>> ListarActivosAsync(CancellationToken cancellationToken)
    {
        DateTime utcNow = DateTime.UtcNow;

        return await _dbContext.Set<Combo>()
            .AsNoTracking()
            .Where(c => !c.Baja)
            .OrderBy(c => c.Nombre)
            .Select(c => new ComboListadoDto(
                c.CombosID,
                c.Nombre,
                c.Codigo,
                c.MarcaProducto != null ? c.MarcaProducto.Nombre : string.Empty,
                c.FechaInicio,
                c.FechaVigencia,
                c.FechaInicio <= utcNow && c.FechaVigencia >= utcNow,
                c.Baja
            ))
            .ToListAsync(cancellationToken);
    }
}
