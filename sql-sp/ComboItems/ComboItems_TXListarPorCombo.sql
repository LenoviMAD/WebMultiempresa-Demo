CREATE OR ALTER PROCEDURE [dbo].[ComboItems_TXListarPorCombo]
    @CombosID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ci.[ComboItemsID],
           ci.[CombosID],
           ci.[ProductosID],
           p.[Nombre]   AS [NombreProducto],
           p.[Codigo]   AS [CodigoProducto],
           ci.[Cantidad],
           ci.[Tipo],
           ci.[Precio],
           ci.[Descuento1],
           ci.[Descuento2],
           ci.[NroGrupoDinamico]
    FROM   [dbo].[ComboItems] ci
    INNER JOIN [dbo].[Productos] p
           ON  p.[ProductosID] = ci.[ProductosID]
    WHERE  ci.[CombosID] = @CombosID
    ORDER BY ci.[NroGrupoDinamico], ci.[ComboItemsID];
END
GO
