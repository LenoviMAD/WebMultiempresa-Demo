using WebMultiempresa.Domain.Enums;

namespace WebMultiempresa.Domain.Entities;

public sealed class ActorEstadoLog
{
    public long ActorEstadoLogID { get; private set; }
    public int TiposActoresID { get; private set; }
    public int ActorID { get; private set; }
    public int EmpresaID { get; private set; }
    public TipoEventoActor TipoEvento { get; private set; }
    public DateTime FechaEvento { get; private set; }

    private ActorEstadoLog() { }

    public static ActorEstadoLog Crear(
        int tiposActoresId,
        int actorId,
        int empresaId,
        TipoEventoActor tipoEvento) =>
        new()
        {
            TiposActoresID = tiposActoresId,
            ActorID        = actorId,
            EmpresaID      = empresaId,
            TipoEvento     = tipoEvento,
            FechaEvento    = DateTime.UtcNow
        };
}
