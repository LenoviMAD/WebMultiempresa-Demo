-- Retorna los productos más comprados por cada cliente asignado al vendedor.
-- Replica la lógica de ClientesApi48.GetLoQueMasMeGustaXVendedor() para el nuevo esquema.
-- Usado por DashboardEcom/{vendedorID}/{color}/{empresaID} de la App Vendedores.
CREATE OR ALTER PROCEDURE Clientes_TXMasTeGusta
    @VendedoresID  INT,
    @EmpresaID     INT,
    @DiasAtras     INT = 90,
    @TopXCliente   INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    WITH Agregados AS (
        SELECT
            pd.ProductosID,
            ped.ClientesID,
            SUM(pd.Cantidad) AS TotalCantidad
        FROM PedidosDetalle pd
        INNER JOIN Pedidos ped
            ON ped.PedidosID    = pd.PedidosID
        INNER JOIN ClienteVendedores cv
            ON cv.ClientesID    = ped.ClientesID
            AND cv.VendedoresID = @VendedoresID
            AND cv.EmpresaID    = @EmpresaID
            AND cv.Baja         = 0
        WHERE ped.EmpresaID    = @EmpresaID
          AND ped.VendedoresID = @VendedoresID
          AND ped.Baja         = 0
          AND ped.FechaCarga  >= DATEADD(day, -@DiasAtras, GETUTCDATE())
        GROUP BY pd.ProductosID, ped.ClientesID
    ),
    Rankeados AS (
        SELECT
            ProductosID,
            ClientesID,
            ROW_NUMBER() OVER (PARTITION BY ClientesID ORDER BY TotalCantidad DESC) AS Orden
        FROM Agregados
    )
    SELECT ProductosID, ClientesID, Orden
    FROM   Rankeados
    WHERE  Orden <= @TopXCliente
    ORDER BY ClientesID, Orden;
END
