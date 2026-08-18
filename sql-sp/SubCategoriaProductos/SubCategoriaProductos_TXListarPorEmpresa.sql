-- Retorna subcategorías activas de una empresa con los IDs de categoría asociados (separados por coma).
-- AlertaDeEdad, NivelDeImportancia, EmojiWhatap no existen en el esquema actual.
-- Usado por DashboardEcom/{vendedorID}/{color}/{empresaID} de la App Vendedores.
CREATE OR ALTER PROCEDURE SubCategoriaProductos_TXListarPorEmpresa
    @EmpresaID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        sc.SubCategoriasProductosID,
        sc.Nombre,
        sc.UrlImagen,
        STRING_AGG(CAST(r.CategoriasProductosID AS NVARCHAR(10)), ',')
            WITHIN GROUP (ORDER BY r.CategoriasProductosID) AS CategoriasIDs
    FROM SubCategoriaProductos sc
    LEFT JOIN CategoriaSubCategoriasRelaciones r
        ON r.SubCategoriasProductosID = sc.SubCategoriasProductosID
        AND r.EmpresaID = @EmpresaID
    WHERE sc.EmpresaID = @EmpresaID
      AND sc.Baja      = 0
    GROUP BY
        sc.SubCategoriasProductosID,
        sc.Nombre,
        sc.UrlImagen
    ORDER BY sc.Nombre ASC;
END
