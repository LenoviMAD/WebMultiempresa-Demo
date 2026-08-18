CREATE OR ALTER PROCEDURE [dbo].[MarcasProductos_MActualizar]
    @MarcasProductosID INT,
    @Nombre            NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[MarcasProductos]
    SET    [Nombre] = @Nombre
    WHERE  [MarcasProductosID] = @MarcasProductosID;
END
GO
