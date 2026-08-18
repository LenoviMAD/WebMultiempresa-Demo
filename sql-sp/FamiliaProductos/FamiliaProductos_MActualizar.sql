CREATE OR ALTER PROCEDURE [dbo].[FamiliaProductos_MActualizar]
    @FamiliaProductosID INT,
    @Nombre             NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[FamiliaProductos]
    SET    [Nombre] = @Nombre
    WHERE  [FamiliaProductosID] = @FamiliaProductosID;
END
GO
