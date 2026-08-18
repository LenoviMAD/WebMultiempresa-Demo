CREATE OR ALTER PROCEDURE [dbo].[MarcasProductos_TXListarPorEmpresa]
    @EmpresaID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [MarcasProductosID],
           [EmpresaID],
           [Nombre],
           [Baja]
    FROM   [dbo].[MarcasProductos]
    WHERE  [EmpresaID] = @EmpresaID
      AND  [Baja] = 0
    ORDER BY [Nombre];
END
GO
