CREATE OR ALTER PROCEDURE [dbo].[PedidosJsonMultiempresa_TXEmpresaID]
    @Param1 INT          -- EmpresaID
AS
BEGIN
    SET NOCOUNT ON;

    -- Devuelve pedidos pendientes de descarga en formato JSON compatible
    -- con PedidoJsonMultiempresaItem (SincoApp / EntidadesMultieempresas).
    -- Un pedido es "pendiente" cuando PedidoCerrado = 0 y Baja = 0.
    SELECT
        p.PedidosID          AS PedidosJsonMultiempresaID,
        p.EmpresaID,
        (
            SELECT
                p.VendedoresID                  AS VendedorID,
                p.ClientesID                    AS ClienteID,
                p.EmpresaID                     AS EmpresaID,
                p.ClienteCodigo                 AS ClienteCodigo,
                p.TotalPedido                   AS TotalPedido,
                JSON_QUERY((
                    SELECT
                        ISNULL(pr.Codigo, '')                                               AS CodigoProducto,
                        pd.ProductosID                                                      AS ProductoID,
                        pd.Cantidad                                                         AS Unidades,
                        pd.Cantidad                                                         AS Cantidad,
                        pd.ListasPreciosID                                                  AS [ListadePrecioID],
                        pd.Precio                                                           AS PrecioUnitarioFinal,
                        ROUND(pd.Precio * (1 - pd.Descuento1 / 100.0)
                                        * (1 - pd.Descuento2 / 100.0), 4)                  AS PrecioUnitarioNeto,
                        ROUND(pd.Cantidad * pd.Precio, 2)                                   AS [Total]
                    FROM dbo.PedidosDetalle pd
                    LEFT JOIN dbo.Productos pr
                        ON  pr.ProductosID = pd.ProductosID
                        AND pr.EmpresaID   = p.EmpresaID
                        AND pr.Baja        = 0
                    WHERE pd.PedidosID = p.PedidosID
                    FOR JSON PATH
                ))                              AS LstProductos
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )                    AS JsonPedido,
        p.FechaCreacion      AS HoraGuardada
    FROM dbo.Pedidos p
    WHERE p.EmpresaID     = @Param1
      AND p.PedidoCerrado  = 0
      AND p.Baja           = 0;
END
