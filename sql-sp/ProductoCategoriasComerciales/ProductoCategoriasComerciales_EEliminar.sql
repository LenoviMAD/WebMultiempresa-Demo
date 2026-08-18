CREATE OR ALTER PROCEDURE ProductoCategoriasComerciales_EEliminar
    @ProductosID             INT,
    @CategoriasComercialesID INT,
    @EmpresaID               INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM ProductoCategoriasComerciales
    WHERE ProductosID             = @ProductosID
      AND CategoriasComercialesID = @CategoriasComercialesID
      AND EmpresaID               = @EmpresaID;
END;
