-- Retorna categorías activas de una empresa.
-- UrlImagenMenu y EmojiWhatap se retornan vacíos (columnas no existen en esquema actual).
-- Usado por DashboardEcom/{vendedorID}/{color}/{empresaID} de la App Vendedores.
CREATE OR ALTER PROCEDURE CategoriaProductos_TXListarPorEmpresa
    @EmpresaID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        cp.CategoriasProductosID,
        cp.Nombre,
        cp.UrlImagen,
        ''  AS UrlImagenMenu,
        ''  AS EmojiWhatap
    FROM CategoriaProductos cp
    WHERE cp.EmpresaID = @EmpresaID
      AND cp.Baja      = 0
    ORDER BY cp.Nombre ASC;
END
