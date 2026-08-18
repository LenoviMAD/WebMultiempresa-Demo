CREATE OR ALTER PROCEDURE [dbo].[ListasPrecios_EEliminar]
    @ListasPreciosID INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ListasPrecios]
    SET    [Baja] = 1
    WHERE  [ListasPreciosID] = @ListasPreciosID;
END
GO
