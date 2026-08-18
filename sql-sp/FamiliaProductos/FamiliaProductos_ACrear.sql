CREATE OR ALTER PROCEDURE [dbo].[FamiliaProductos_ACrear]
    @EmpresaID INT,
    @Nombre    NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[FamiliaProductos] ([EmpresaID], [Nombre], [Baja])
    VALUES (@EmpresaID, @Nombre, 0);

    SELECT SCOPE_IDENTITY() AS [FamiliaProductosID];
END
GO
