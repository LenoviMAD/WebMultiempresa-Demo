-- Baja lógica de una subcategoría de producto (no elimina el registro).
CREATE OR ALTER PROCEDURE SubCategoriaProductos_EEliminar
    @SubCategoriasProductosID  INT,
    @EmpresaID                 INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE SubCategoriaProductos
    SET Baja = 1
    WHERE SubCategoriasProductosID = @SubCategoriasProductosID
      AND EmpresaID                = @EmpresaID;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END
