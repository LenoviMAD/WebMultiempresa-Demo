CREATE OR ALTER PROCEDURE ProductoCategoriasComerciales_TXListarPorProducto
    @ProductosID INT,
    @EmpresaID   INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT pc.ProductoCategoriasComercialID,
           pc.ProductosID,
           pc.CategoriasComercialesID,
           c.Nombre AS NombreCategoria
    FROM   ProductoCategoriasComerciales pc
    INNER JOIN CategoriasComerciales c
           ON c.CategoriasComercialesID = pc.CategoriasComercialesID
    WHERE  pc.ProductosID = @ProductosID
      AND  pc.EmpresaID   = @EmpresaID
      AND  c.Baja = 0
    ORDER BY c.Nombre;
END;
