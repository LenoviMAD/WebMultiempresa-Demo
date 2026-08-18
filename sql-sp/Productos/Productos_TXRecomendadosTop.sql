-- Retorna los productos más vendidos por cantidad en los últimos @DiasAtras días para una empresa.
-- Replica la lógica de ProductosMultiEmpresa.GetRecomendados(empresaID) para el nuevo esquema.
-- Usado por DashboardEcom/{vendedorID}/{color}/{empresaID} de la App Vendedores.
CREATE OR ALTER PROCEDURE Productos_TXRecomendadosTop
    @EmpresaID  INT,
    @DiasAtras  INT = 90,
    @TopN       INT = 50
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (@TopN)
        pr.ProductosID,
        pr.Nombre,
        ROW_NUMBER() OVER (ORDER BY SUM(pd.Cantidad) DESC) AS Orden
    FROM PedidosDetalle pd
    INNER JOIN Pedidos   ped ON ped.PedidosID  = pd.PedidosID
    INNER JOIN Productos pr  ON pr.ProductosID = pd.ProductosID
    WHERE ped.EmpresaID              = @EmpresaID
      AND ped.Baja                   = 0
      AND ped.FechaCarga            >= DATEADD(day, -@DiasAtras, GETUTCDATE())
      AND pr.EmpresaID               = @EmpresaID
      AND pr.Baja                    = 0
      AND pr.DesactivadoParaLaVenta  = 0
    GROUP BY pr.ProductosID, pr.Nombre
    ORDER BY SUM(pd.Cantidad) DESC;
END
