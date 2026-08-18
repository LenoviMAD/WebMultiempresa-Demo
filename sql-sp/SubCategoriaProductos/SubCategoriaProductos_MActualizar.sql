-- Actualiza los datos de una subcategoría de producto.
-- UrlImagenMenu, NivelDeImportancia, AlertaDeEdad, EmojiWhatap no existen en el esquema actual.
CREATE OR ALTER PROCEDURE SubCategoriaProductos_MActualizar
    @SubCategoriasProductosID  INT,
    @EmpresaID                 INT,
    @Nombre                    NVARCHAR(150),
    @UrlImagen                 NVARCHAR(350) = ''
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE SubCategoriaProductos
    SET
        Nombre    = @Nombre,
        UrlImagen = ISNULL(@UrlImagen, '')
    WHERE SubCategoriasProductosID = @SubCategoriasProductosID
      AND EmpresaID                = @EmpresaID
      AND Baja                     = 0;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END
