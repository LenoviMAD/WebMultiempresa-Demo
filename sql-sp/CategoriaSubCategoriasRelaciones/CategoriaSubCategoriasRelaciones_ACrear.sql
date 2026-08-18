-- Asocia una subcategoría a una categoría dentro de una empresa.
-- Retorna el ID generado. Si la relación ya existe, no inserta (IGNORE via NOT EXISTS).
CREATE OR ALTER PROCEDURE CategoriaSubCategoriasRelaciones_ACrear
    @EmpresaID                 INT,
    @CategoriasProductosID     INT,
    @SubCategoriasProductosID  INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 FROM CategoriaSubCategoriasRelaciones
        WHERE EmpresaID                = @EmpresaID
          AND CategoriasProductosID    = @CategoriasProductosID
          AND SubCategoriasProductosID = @SubCategoriasProductosID
    )
    BEGIN
        INSERT INTO CategoriaSubCategoriasRelaciones
            (EmpresaID, CategoriasProductosID, SubCategoriasProductosID)
        VALUES
            (@EmpresaID, @CategoriasProductosID, @SubCategoriasProductosID);

        SELECT SCOPE_IDENTITY() AS CategoriaSubCategoriaRelacionID;
    END
    ELSE
    BEGIN
        SELECT CategoriaSubCategoriaRelacionID
        FROM CategoriaSubCategoriasRelaciones
        WHERE EmpresaID                = @EmpresaID
          AND CategoriasProductosID    = @CategoriasProductosID
          AND SubCategoriasProductosID = @SubCategoriasProductosID;
    END
END
