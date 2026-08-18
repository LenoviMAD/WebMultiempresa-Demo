-- Retorna metadatos de sincronización por producto: stock, precio, nueva incorporación.
-- @FechaDesde NULL = todos; con fecha = solo productos o precios modificados desde esa fecha.
-- Usado por el endpoint ProductosActualItems/{fechaDesde}/{empresaID} de la App Vendedores.
--
-- DiasStkDisp : 99 si hay stock, 0 si no hay stock
-- DiasPrecMod : días desde la última actualización de precio (9999 si no tiene precio)
-- NuevaIncor  : 1 si el producto fue dado de alta hace menos de 7 días
-- IngreRec    : 0 (sin historial de ventas en multiempresa v1)
-- ComisDif    : 0 (sin comisiones diferenciales en multiempresa v1)
CREATE OR ALTER PROCEDURE Productos_TXActualItems
    @EmpresaID  INT,
    @FechaDesde DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.ProductosID,
        CASE WHEN p.StockEnUnidades > 0 THEN 99 ELSE 0 END                          AS DiasStkDisp,
        ISNULL(DATEDIFF(day, MAX(pp.FechaActualizacion), GETUTCDATE()), 9999)        AS DiasPrecMod,
        CASE WHEN DATEDIFF(day, p.FechaAlta, GETUTCDATE()) < 7 THEN 1 ELSE 0 END   AS NuevaIncor,
        CAST(0 AS BIT)                                                               AS IngreRec,
        CAST(0 AS BIT)                                                               AS ComisDif
    FROM Productos p
    LEFT JOIN ProductoPrecios pp
        ON  pp.ProductosID = p.ProductosID
        AND pp.EmpresaID   = @EmpresaID
        AND pp.Baja        = 0
    WHERE p.EmpresaID = @EmpresaID
      AND p.Baja      = 0
    GROUP BY
        p.ProductosID,
        p.StockEnUnidades,
        p.FechaAlta,
        p.FechaActualizacion
    HAVING
        @FechaDesde IS NULL
        OR p.FechaActualizacion        >= @FechaDesde
        OR MAX(pp.FechaActualizacion)  >= @FechaDesde;
END
