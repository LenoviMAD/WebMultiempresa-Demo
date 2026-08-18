CREATE OR ALTER PROCEDURE CategoriasComerciales_MActualizar
    @CategoriasComercialesID INT,
    @EmpresaID               INT,
    @Nombre                  NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CategoriasComerciales
    SET Nombre = @Nombre
    WHERE CategoriasComercialesID = @CategoriasComercialesID
      AND EmpresaID               = @EmpresaID
      AND Baja = 0;
END;
