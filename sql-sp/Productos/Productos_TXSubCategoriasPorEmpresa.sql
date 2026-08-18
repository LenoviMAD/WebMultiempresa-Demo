-- Retorna las relaciones producto → subcategoría para una empresa.
-- Replica SubCategoriasProductosRelacion.SubCategoriasProductosRelacionItems() para el nuevo esquema.
-- Usado por bloqueadosYSubcategorias/{vendedorID}/{empresaID} de la App Vendedores.
CREATE OR ALTER PROCEDURE Productos_TXSubCategoriasPorEmpresa
    @EmpresaID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        psc.ProductosID,
        psc.SubCategoriasProductosID
    FROM ProductoSubCategorias psc
    INNER JOIN Productos p
        ON  p.ProductosID = psc.ProductosID
        AND p.EmpresaID   = @EmpresaID
        AND p.Baja        = 0
        AND p.DesactivadoParaLaVenta = 0
    WHERE psc.EmpresaID = @EmpresaID
      AND psc.Baja      = 0
    ORDER BY psc.ProductosID;
END
