-- Retorna stock vendible de todos los productos activos de una empresa.
-- Lógica padre/hijo equivalente a la legacy:
--   StockPropio = 1 → FLOOR(propio.StockEnUnidades / propio.UnidadesPorBulto)
--   StockPropio = 0 → FLOOR(padre.StockEnUnidades  / hijo.UnidadesPorBulto)
CREATE OR ALTER PROCEDURE Productos_TXStock
    @EmpresaID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.ProductosID AS id,
        CAST(FLOOR(
            CAST(
                CASE WHEN p.StockPropio = 1 THEN p.StockEnUnidades
                     ELSE ISNULL(
                         (SELECT p2.StockEnUnidades
                          FROM   Productos p2
                          WHERE  p2.ProductosID = p.ProductosIDPadre
                            AND  p2.EmpresaID   = @EmpresaID
                            AND  p2.Baja        = 0),
                         0)
                END AS FLOAT
            ) / NULLIF(p.UnidadesPorBulto, 0)
        ) AS INT) AS stk
    FROM  Productos p
    WHERE p.EmpresaID = @EmpresaID
      AND p.Baja      = 0;
END
