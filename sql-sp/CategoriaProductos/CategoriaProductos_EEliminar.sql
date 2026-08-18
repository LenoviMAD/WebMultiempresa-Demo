-- Baja lógica de una categoría de producto (no elimina el registro).
CREATE OR ALTER PROCEDURE CategoriaProductos_EEliminar
    @CategoriasProductosID  INT,
    @EmpresaID              INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CategoriaProductos
    SET Baja = 1
    WHERE CategoriasProductosID = @CategoriasProductosID
      AND EmpresaID             = @EmpresaID;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END
