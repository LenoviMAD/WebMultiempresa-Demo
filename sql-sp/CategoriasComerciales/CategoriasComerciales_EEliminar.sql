CREATE OR ALTER PROCEDURE CategoriasComerciales_EEliminar
    @CategoriasComercialesID INT,
    @EmpresaID               INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CategoriasComerciales
    SET Baja = 1
    WHERE CategoriasComercialesID = @CategoriasComercialesID
      AND EmpresaID               = @EmpresaID;
END;
