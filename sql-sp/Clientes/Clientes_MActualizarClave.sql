-- Actualiza el hash de clave del cliente (portal web / app autogestión).
-- Reemplaza el SP legacy Clientes_MClaveWeb para empresas con EmpresaID > 0.
-- La API es responsable de pasar el hash BCrypt ya generado.
CREATE OR ALTER PROCEDURE Clientes_MActualizarClave
    @ClientesID  INT,
    @EmpresaID   INT,
    @ClaveHash   NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Clientes
    SET    ClaveHash = @ClaveHash
    WHERE  ClientesID = @ClientesID
      AND  EmpresaID  = @EmpresaID
      AND  Baja       = 0;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END
