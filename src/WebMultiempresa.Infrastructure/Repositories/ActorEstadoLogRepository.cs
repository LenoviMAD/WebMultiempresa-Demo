using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class ActorEstadoLogRepository : IActorEstadoLogRepository
{
    private readonly AppDbContext _dbContext;

    public ActorEstadoLogRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task RegistrarAsync(ActorEstadoLog log, CancellationToken cancellationToken)
    {
        await _dbContext.ActorEstadoLogs.AddAsync(log, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> ContarActivosPorMesAsync(
        int tiposActoresId,
        int empresaId,
        DateTime inicioMes,
        DateTime finMes,
        CancellationToken cancellationToken)
    {
        List<ConteoRow> rows = await _dbContext.Database
            .SqlQuery<ConteoRow>(
                $"EXEC Actores_TXContarActivosPorMes @TiposActoresID = {tiposActoresId}, @EmpresaID = {empresaId}, @InicioMes = {inicioMes}, @FinMes = {finMes}")
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return rows.FirstOrDefault()?.CantidadActivos ?? 0;
    }

    private sealed class ConteoRow
    {
        public int CantidadActivos { get; set; }
    }
}
