-- Actualiza la clave en texto plano de un vendedor multiempresa.
CREATE OR ALTER PROCEDURE Vendedores_MActualizarClave
    @VendedoresID  INT,
    @EmpresaID     INT,
    @Clave         NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Vendedores
    SET    Clave = @Clave
    WHERE  VendedoresID = @VendedoresID
      AND  EmpresaID    = @EmpresaID
      AND  Baja         = 0;

    SELECT @@ROWCOUNT AS FilasAfectadas;
END
