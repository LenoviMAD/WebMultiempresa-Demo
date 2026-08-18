CREATE OR ALTER PROCEDURE [dbo].[ListasPrecios_MActualizar]
    @ListasPreciosID  INT,
    @Nombre           NVARCHAR(100),
    @PorcentajeMarcup DECIMAL(5,2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ListasPrecios]
    SET    [Nombre]           = @Nombre,
           [PorcentajeMarcup] = @PorcentajeMarcup
    WHERE  [ListasPreciosID] = @ListasPreciosID;
END
GO
