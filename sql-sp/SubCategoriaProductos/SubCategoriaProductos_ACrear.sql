-- Crea una nueva subcategoría de producto para una empresa.
-- UrlImagenMenu, NivelDeImportancia, AlertaDeEdad, EmojiWhatap no existen en el esquema actual.
CREATE OR ALTER PROCEDURE SubCategoriaProductos_ACrear
    @EmpresaID   INT,
    @Nombre      NVARCHAR(150),
    @UrlImagen   NVARCHAR(350) = ''
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO SubCategoriaProductos
        (EmpresaID, Nombre, UrlImagen, Baja)
    VALUES
        (@EmpresaID, @Nombre, ISNULL(@UrlImagen, ''), 0);

    SELECT SCOPE_IDENTITY() AS SubCategoriasProductosID;
END
