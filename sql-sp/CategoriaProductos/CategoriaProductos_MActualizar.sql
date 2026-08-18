-- Actualiza los datos de una categoría de producto.
-- UrlImagenMenu, NivelDeImportancia, EmojiWhatap no existen en el esquema actual.
CREATE OR ALTER PROCEDURE CategoriaProductos_MActualizar
    @CategoriasProductosID  INT,
    @EmpresaID              INT,
    @Nombre                 NVARCHAR(150),
    @UrlImagen              NVARCHAR(350) = ''
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CategoriaProductos
    SET
        Nombre    = @Nombre,
        UrlImagen = ISNULL(@UrlImagen, '')
    WHERE CategoriasProductosID = @CategoriasProductosID
      AND EmpresaID             = @EmpresaID
      AND Baja                  = 0;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END
