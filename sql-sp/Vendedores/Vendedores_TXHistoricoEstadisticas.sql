-- Devuelve el histórico de estadísticas de un vendedor en un rango de fechas.
-- Incluye la cantidad de estrellas encendidas por día (calculada).
CREATE OR ALTER PROCEDURE Vendedores_TXHistoricoEstadisticas
    @VendedoresID   INT,
    @FechaDesde     DATE,
    @FechaHasta     DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        e.VendedorEstadisticasID,
        e.Fecha,
        e.TotalVenta,
        e.TotalDevolucion,
        e.ClientesConVisitas,
        e.ClientesConVentas,
        e.ComisionPorDia,
        e.HorasEnLaRuta,
        e.CoeficienteComision,
        (SELECT COUNT(*) FROM VendedorEstrellasDiarias ed
         WHERE ed.VendedorEstadisticasID = e.VendedorEstadisticasID
           AND ed.EstaEncendida = 1) AS EstrellasDelDia
    FROM VendedorEstadisticas e
    WHERE e.VendedoresID = @VendedoresID
      AND e.Fecha        BETWEEN @FechaDesde AND @FechaHasta
      AND e.Baja         = 0
    ORDER BY e.Fecha DESC;
END
