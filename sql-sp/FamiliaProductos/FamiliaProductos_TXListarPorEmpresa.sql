CREATE OR ALTER PROCEDURE [dbo].[FamiliaProductos_TXListarPorEmpresa]
    @EmpresaID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [FamiliaProductosID],
           [EmpresaID],
           [Nombre],
           [Baja]
    FROM   [dbo].[FamiliaProductos]
    WHERE  [EmpresaID] = @EmpresaID
      AND  [Baja] = 0
    ORDER BY [Nombre];
END
GO
