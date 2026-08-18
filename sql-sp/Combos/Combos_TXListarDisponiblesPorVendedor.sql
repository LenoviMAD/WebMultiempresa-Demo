-- Retorna combos disponibles por cliente para un vendedor multiempresa.
-- Una fila por (ClientesID, CombosID) para todos los combos activos y vigentes
-- del vendedor. CantidadYaComprada = 0 (sin historial en multiempresa v1).
-- Usado por el endpoint combosxcliente/{vendedoresID}/{empresaID} de la App Vendedores.
CREATE OR ALTER PROCEDURE Combos_TXListarDisponiblesPorVendedor
    @VendedoresID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @EmpresaID INT;
    SELECT @EmpresaID = EmpresaID
    FROM   Vendedores
    WHERE  VendedoresID = @VendedoresID;

    IF @EmpresaID IS NULL RETURN;

    SELECT
        cv.ClientesID                AS ClientesID,
        c.CombosID                   AS CombosID,
        0                            AS Orden,
        0                            AS CategoriaDeCombos,
        0                            AS CantidadYaComprada
    FROM Combos c
    INNER JOIN ClienteVendedores cv
        ON cv.EmpresaID    = @EmpresaID
        AND cv.VendedoresID = @VendedoresID
        AND cv.Baja        = 0
    WHERE c.EmpresaID  = @EmpresaID
      AND c.Baja       = 0
      AND c.FechaVigencia >= GETUTCDATE();
END
