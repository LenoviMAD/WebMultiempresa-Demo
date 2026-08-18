-- Retorna IDs de productos dados de alta en los últimos @DiasAtras días.
-- Usado por el endpoint nuevosingresos/{empresaID} de la App Vendedores.
CREATE OR ALTER PROCEDURE Productos_TXNuevosIngresos
    @EmpresaID  INT,
    @DiasAtras  INT = 30
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ProductosID
    FROM   Productos
    WHERE  EmpresaID              = @EmpresaID
      AND  Baja                   = 0
      AND  DesactivadoParaLaVenta = 0
      AND  FechaAlta             >= DATEADD(day, -@DiasAtras, GETUTCDATE())
    ORDER BY FechaAlta DESC;
END
