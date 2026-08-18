-- Retorna precios por lista para todos los productos de una empresa.
-- @FechaDesde NULL = todos; con fecha = solo precios actualizados desde esa fecha.
-- Usado por el endpoint PreciosProductos/{fechaDesde}/{empresaID} de la App Vendedores.
CREATE OR ALTER PROCEDURE Productos_TXPrecios
    @EmpresaID  INT,
    @FechaDesde DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        pp.ProductosID                                                                          AS ProdID,
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = 3 THEN pp.PrecioFinal ELSE 0 END), 2)         AS PFAut,
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = 1 THEN pp.PrecioFinal ELSE 0 END), 2)         AS PFEsp,
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = 5 THEN pp.PrecioFinal ELSE 0 END), 2)         AS PFAl,
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = 6 THEN pp.PrecioFinal ELSE 0 END), 2)         AS PFFKi,
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = 4 THEN pp.PrecioFinal ELSE 0 END), 2)         AS PFSal,
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = 7 THEN pp.PrecioFinal ELSE 0 END), 2)         AS PFEcom
    FROM ProductoPrecios pp
    INNER JOIN Productos p ON p.ProductosID = pp.ProductosID AND p.Baja = 0
    WHERE pp.EmpresaID = @EmpresaID
      AND pp.Baja      = 0
      AND (@FechaDesde IS NULL OR pp.FechaActualizacion >= @FechaDesde)
    GROUP BY pp.ProductosID;
END
