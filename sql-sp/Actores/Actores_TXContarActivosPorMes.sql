CREATE OR ALTER PROCEDURE [dbo].[Actores_TXContarActivosPorMes]
    @TiposActoresID INT,
    @EmpresaID      INT,
    @InicioMes      DATETIME2,
    @FinMes         DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    -- Cuenta actores distintos que estuvieron activos en cualquier momento del mes.
    -- Un actor cuenta si alguno de sus períodos (Alta → Baja o Alta → hoy)
    -- se superpone con el intervalo [@InicioMes, @FinMes].
    WITH Periodos AS (
        SELECT
            alta.ActorID,
            alta.FechaEvento AS InicioActivacion,
            (
                SELECT MIN(baja.FechaEvento)
                FROM ActorEstadoLog baja
                WHERE baja.ActorID        = alta.ActorID
                  AND baja.TiposActoresID = alta.TiposActoresID
                  AND baja.TipoEvento     = 'Baja'
                  AND baja.FechaEvento    > alta.FechaEvento
            ) AS FinActivacion
        FROM ActorEstadoLog alta
        WHERE alta.TipoEvento     = 'Alta'
          AND alta.TiposActoresID = @TiposActoresID
          AND alta.EmpresaID      = @EmpresaID
    )
    SELECT COUNT(DISTINCT ActorID) AS CantidadActivos
    FROM Periodos
    WHERE InicioActivacion <= @FinMes
      AND (FinActivacion IS NULL OR FinActivacion >= @InicioMes);
END
