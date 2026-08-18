-- Elimina físicamente la relación entre una categoría y una subcategoría.
-- Las relaciones son datos estructurales sin historial; la eliminación es física.
CREATE OR ALTER PROCEDURE CategoriaSubCategoriasRelaciones_EEliminar
    @EmpresaID                 INT,
    @CategoriasProductosID     INT,
    @SubCategoriasProductosID  INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM CategoriaSubCategoriasRelaciones
    WHERE EmpresaID                = @EmpresaID
      AND CategoriasProductosID    = @CategoriasProductosID
      AND SubCategoriasProductosID = @SubCategoriasProductosID;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END
