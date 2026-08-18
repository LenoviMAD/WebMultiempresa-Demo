-- Lista productos con TODOS los precios disponibles (L1..L7) para una empresa.
-- Reemplaza FuncionesAppVendedores.GetProductos() para EmpresaID > 0.
-- Retorna una fila por producto con precios pivotados para que el controller
-- pueda setear PrecioUnitarioFinalXxx y TextoPrecioCalculadoN en ProductoListaItem.
-- @ListasPreciosID : lista principal del cliente, usada para el campo PrecioUnitarioFinal.
-- @FechaDesde      : NULL = todos; con fecha = productos o precios modificados desde esa fecha.
-- NOTA naming: Rubro = MarcasProductos, Marca = FamiliaProductos.
--
-- Lógica de stock (equivalente a la legacy ValidarStock):
--   StockPropio = 1 → stock = FLOOR(propio.StockEnUnidades / propio.UnidadesPorBulto)
--   StockPropio = 0 → stock = FLOOR(padre.StockEnUnidades  / hijo.UnidadesPorBulto)
--   El OUTER APPLY "stkPadre" resuelve el stock físico del padre cuando StockPropio=0.
CREATE OR ALTER PROCEDURE Productos_TXListarConPrecios
    @ListasPreciosID  INT,
    @EmpresaID        INT,
    @FechaDesde       DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.ProductosID,
        ISNULL(p.ProductosIDPadre, 0)                                                AS ProductosIDPadre,
        p.Codigo                                                                      AS CodigoDeProducto,
        p.Nombre,
        -- Stock vendible: hijos sin stock propio usan el físico del padre / sus unidades por bulto
        CAST(FLOOR(
            CAST(
                CASE WHEN p.StockPropio = 1 THEN p.StockEnUnidades
                     ELSE ISNULL(stkPadre.StockFisico, 0)
                END AS FLOAT
            ) / NULLIF(p.UnidadesPorBulto, 0)
        ) AS INT)                                                                     AS StockEnUnidades,
        p.CantidadMultiplo,
        p.UnidadesPorBulto,
        p.DesactivadoParaLaVenta,
        p.StockPropio,
        ISNULL(p.UrlImagenWeb, '')                                                    AS UrlImagenWeb,
        ISNULL(mp.Nombre, '')                                                         AS Rubro,
        ISNULL(fp.Nombre, '')                                                         AS Marca,
        ISNULL((
            SELECT TOP 1 pc2.CategoriasComercialesID
            FROM   ProductoCategoriasComerciales pc2
            WHERE  pc2.ProductosID = p.ProductosID
              AND  pc2.EmpresaID   = @EmpresaID
            ORDER BY pc2.ProductoCategoriasComercialID
        ), 0)                                                                         AS CategoriaComercial,
        ISNULL(imp.Porcentaje, 0)                                                     AS PorcentajeDeIva,
        -- Precios por lista (pivot manual; 0 si no existe esa lista para el producto)
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = 1 THEN pp.PrecioFinal ELSE 0 END), 2)         AS PrecioL1,
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = 3 THEN pp.PrecioFinal ELSE 0 END), 2)         AS PrecioL3,
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = 4 THEN pp.PrecioFinal ELSE 0 END), 2)         AS PrecioL4,
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = 5 THEN pp.PrecioFinal ELSE 0 END), 2)         AS PrecioL5,
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = 6 THEN pp.PrecioFinal ELSE 0 END), 2)         AS PrecioL6,
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = 7 THEN pp.PrecioFinal ELSE 0 END), 2)         AS PrecioL7,
        -- Precio primario: lista principal del cliente
        ROUND(MAX(CASE WHEN pp.ListasPreciosID = @ListasPreciosID THEN pp.PrecioFinal ELSE 0 END), 2) AS PrecioUnitarioFinal,
        @ListasPreciosID                                                              AS ListaDePreciosUsada,
        -- Días de stock disponible basado en stock efectivo (propio o del padre)
        CASE WHEN
            CASE WHEN p.StockPropio = 1 THEN p.StockEnUnidades
                 ELSE ISNULL(stkPadre.StockFisico, 0)
            END > 0
        THEN 9999 ELSE 0 END                                                         AS DiasDeStockDisponible,
        -- Días desde la última modificación de precio; 9999 si nunca fue modificado
        ISNULL(DATEDIFF(DAY, MAX(pp.FechaActualizacion), GETDATE()), 9999)            AS DiasDePrecioModificado,
        -- Producto nuevo si fue dado de alta hace menos de 8 días,
        -- o si reingresó al stock (pasó de 0 a > 0) hace menos de 8 días
        CASE WHEN DATEDIFF(DAY, p.FechaAlta,             GETDATE()) < 8 THEN 1
             WHEN p.FechaUltimoReingreso IS NOT NULL
              AND DATEDIFF(DAY, p.FechaUltimoReingreso, GETDATE()) < 8 THEN 1
             ELSE 0
        END                                                                           AS NuevaIncorporacion
    FROM Productos p
    -- Cuando StockPropio=0, trae el stock físico del padre para calcular unidades vendibles
    OUTER APPLY (
        SELECT p2.StockEnUnidades AS StockFisico
        FROM   Productos p2
        WHERE  p2.ProductosID = p.ProductosIDPadre
          AND  p2.EmpresaID   = @EmpresaID
          AND  p2.Baja        = 0
    ) AS stkPadre
    -- Requiere que el producto tenga al menos un precio en cualquier lista
    INNER JOIN (
        SELECT DISTINCT ProductosID
        FROM   ProductoPrecios
        WHERE  EmpresaID = @EmpresaID
          AND  Baja      = 0
    ) pp_any ON pp_any.ProductosID = p.ProductosID
    -- LEFT JOIN para obtener todos los precios disponibles
    LEFT JOIN ProductoPrecios pp
        ON  pp.ProductosID = p.ProductosID
        AND pp.EmpresaID   = @EmpresaID
        AND pp.Baja        = 0
    LEFT JOIN MarcasProductos mp
        ON  mp.MarcasProductosID = p.MarcasProductosID
        AND mp.Baja              = 0
    LEFT JOIN FamiliaProductos fp
        ON  fp.FamiliaProductosID = p.FamiliaProductosID
        AND fp.Baja               = 0
    LEFT JOIN Impuestos imp
        ON  imp.ImpuestosID = p.ImpuestosID
        AND imp.EmpresaID   = @EmpresaID
        AND imp.Baja        = 0
    WHERE p.EmpresaID = @EmpresaID
      AND p.Baja      = 0
    GROUP BY
        p.ProductosID, p.ProductosIDPadre, p.Codigo, p.Nombre,
        p.StockEnUnidades, p.CantidadMultiplo, p.UnidadesPorBulto,
        p.DesactivadoParaLaVenta, p.StockPropio, p.UrlImagenWeb,
        p.MarcasProductosID, p.FamiliaProductosID, p.ImpuestosID,
        p.FechaActualizacion, p.FechaAlta, p.FechaUltimoReingreso,
        mp.Nombre, fp.Nombre, imp.Porcentaje,
        stkPadre.StockFisico
    HAVING
        @FechaDesde IS NULL
        OR p.FechaActualizacion                                       >= @FechaDesde
        OR MAX(CASE WHEN pp.FechaActualizacion >= @FechaDesde THEN 1 ELSE 0 END) = 1
    ORDER BY p.Nombre ASC;
END
