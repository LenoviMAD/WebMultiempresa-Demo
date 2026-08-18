CREATE OR ALTER PROCEDURE CategoriasComerciales_TXListarPorEmpresa
    @EmpresaID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT CategoriasComercialesID,
           EmpresaID,
           Nombre,
           Baja
    FROM   CategoriasComerciales
    WHERE  EmpresaID = @EmpresaID
      AND  Baja = 0
    ORDER BY Nombre;
END;
