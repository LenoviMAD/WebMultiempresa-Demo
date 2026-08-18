-- Lista las definiciones de estrellas activas para una empresa.
-- La app usa esto para saber cuántas estrellas hay y qué mide cada una.
CREATE OR ALTER PROCEDURE Vendedores_TXEstrellasDefiniciones
    @EmpresaID  INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ed.VendedorEstrellasDefinicionesID,
        ed.NumeroEstrella,
        ed.Nombre,
        ed.ObjetivoMensual
    FROM VendedorEstrellasDefiniciones ed
    WHERE ed.EmpresaID = @EmpresaID
      AND ed.Baja      = 0
    ORDER BY ed.NumeroEstrella;
END
