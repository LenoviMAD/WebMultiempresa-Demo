CREATE OR ALTER PROCEDURE Impuestos_TXListarPorEmpresa
    @EmpresaID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ImpuestosID,
           EmpresaID,
           Nombre,
           Porcentaje,
           Baja
    FROM   Impuestos
    WHERE  EmpresaID = @EmpresaID
      AND  Baja = 0
    ORDER BY Porcentaje;
END;
