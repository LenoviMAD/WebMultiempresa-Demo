CREATE OR ALTER PROCEDURE ProductoCategoriasComerciales_AAgregar
    @ProductosID             INT,
    @CategoriasComercialesID INT,
    @EmpresaID               INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 FROM ProductoCategoriasComerciales
        WHERE ProductosID             = @ProductosID
          AND CategoriasComercialesID = @CategoriasComercialesID
    )
    BEGIN
        INSERT INTO ProductoCategoriasComerciales (ProductosID, CategoriasComercialesID, EmpresaID)
        VALUES (@ProductosID, @CategoriasComercialesID, @EmpresaID);
    END;
END;
