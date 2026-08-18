CREATE OR ALTER PROCEDURE [dbo].[ListasPrecios_ACrear]
    @EmpresaID        INT,
    @Nombre           NVARCHAR(100),
    @PorcentajeMarcup DECIMAL(5,2) = 0
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[ListasPrecios] ([EmpresaID], [Nombre], [PorcentajeMarcup], [Baja])
    VALUES (@EmpresaID, @Nombre, @PorcentajeMarcup, 0);

    SELECT SCOPE_IDENTITY() AS [ListasPreciosID];
END
GO
