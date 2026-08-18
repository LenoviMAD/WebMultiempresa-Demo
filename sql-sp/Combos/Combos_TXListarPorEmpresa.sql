CREATE OR ALTER PROCEDURE [dbo].[Combos_TXListarPorEmpresa]
    @EmpresaID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT c.[CombosID],
           c.[EmpresaID],
           c.[MarcasProductosID],
           r.[Nombre]       AS [NombreMarcaProducto],
           c.[Nombre],
           c.[Codigo],
           c.[FechaInicio],
           c.[FechaVigencia],
           c.[Baja]
    FROM   [dbo].[Combos] c
    LEFT JOIN [dbo].[MarcasProductos] r ON r.[MarcasProductosID] = c.[MarcasProductosID]
    WHERE  c.[EmpresaID] = @EmpresaID
      AND  c.[Baja] = 0
    ORDER BY c.[Nombre];
END
GO
