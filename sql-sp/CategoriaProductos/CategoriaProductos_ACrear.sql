-- Crea una nueva categoría de producto para una empresa.
-- UrlImagenMenu, NivelDeImportancia, EmojiWhatap no existen en el esquema actual.
CREATE OR ALTER PROCEDURE CategoriaProductos_ACrear
    @EmpresaID   INT,
    @Nombre      NVARCHAR(150),
    @UrlImagen   NVARCHAR(350) = ''
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO CategoriaProductos
        (EmpresaID, Nombre, UrlImagen, Baja)
    VALUES
        (@EmpresaID, @Nombre, ISNULL(@UrlImagen, ''), 0);

    SELECT SCOPE_IDENTITY() AS CategoriasProductosID;
END
