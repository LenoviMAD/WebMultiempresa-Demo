CREATE OR ALTER PROCEDURE [dbo].[MarcasProductos_ACrear]
    @EmpresaID INT,
    @Nombre    NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[MarcasProductos] ([EmpresaID], [Nombre], [Baja])
    VALUES (@EmpresaID, @Nombre, 0);

    SELECT SCOPE_IDENTITY() AS [MarcasProductosID];
END
GO
