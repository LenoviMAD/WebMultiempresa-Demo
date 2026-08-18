-- Devuelve los datos del vendedor por código + empresa para que la API verifique
-- la clave en código.
CREATE OR ALTER PROCEDURE Vendedores_TXValidarAcceso
    @Codigo     NVARCHAR(50),
    @EmpresaID  INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.VendedoresID,
        v.Nombre,
        v.Codigo,
        v.WhatsApp,
        v.CategoriasComercialesID,
        v.VerTodasLasCategorias,
        v.TiposDeVendedoresID,
        v.DiaInicioRuta,
        v.Clave
    FROM Vendedores v
    WHERE v.Codigo    = @Codigo
      AND v.EmpresaID = @EmpresaID
      AND v.Baja      = 0;
END
