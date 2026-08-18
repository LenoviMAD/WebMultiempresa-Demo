-- Retorna los mensajes no leídos y no dados de baja de un vendedor para una empresa.
-- Equivalente a FuncionesAppVendedoresApi.GetMensajeaVD() del ERP legacy.
-- Por ahora no se usa activamente; esquema listo para implementación futura.
CREATE OR ALTER PROCEDURE MensajesVendedor_TXListarPorVendedor
    @EmpresaID   INT,
    @VendedoresID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        MensajesVendedorID AS MensajeID,
        Bloqueado,
        ISNULL(Subject, '')  AS Subject,
        ISNULL(Mensaje, '')  AS Mensage,
        Leido,
        FechaCarga           AS Fecha,
        VendedoresID         AS VendedorID
    FROM  MensajesVendedor
    WHERE EmpresaID   = @EmpresaID
      AND VendedoresID = @VendedoresID
      AND Leido        = 0
      AND Baja         = 0
    ORDER BY FechaCarga DESC;
END
