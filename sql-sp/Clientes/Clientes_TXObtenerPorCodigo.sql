-- Devuelve el ClientesID a partir del código de cliente y empresa.
-- Usado por el endpoint GET /clienteitem/idClienteXCodigo/{codigo}.
CREATE OR ALTER PROCEDURE Clientes_TXObtenerPorCodigo
    @Codigo     NVARCHAR(50),
    @EmpresaID  INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ClientesID
    FROM   Clientes
    WHERE  Codigo    = @Codigo
      AND  EmpresaID = @EmpresaID
      AND  Baja      = 0;
END
