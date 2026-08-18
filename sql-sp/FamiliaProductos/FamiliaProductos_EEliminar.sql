CREATE OR ALTER PROCEDURE [dbo].[FamiliaProductos_EEliminar]
    @FamiliaProductosID INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[FamiliaProductos]
    SET    [Baja] = 1
    WHERE  [FamiliaProductosID] = @FamiliaProductosID;
END
GO
