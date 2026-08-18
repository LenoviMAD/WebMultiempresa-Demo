CREATE OR ALTER PROCEDURE CategoriasComerciales_ACrear
    @EmpresaID INT,
    @Nombre    NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO CategoriasComerciales (EmpresaID, Nombre, Baja)
    VALUES (@EmpresaID, @Nombre, 0);

    SELECT SCOPE_IDENTITY() AS CategoriasComercialesID;
END;
