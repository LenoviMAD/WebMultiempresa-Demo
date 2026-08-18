CREATE OR ALTER PROCEDURE Impuestos_EEliminar
    @ImpuestosID INT,
    @EmpresaID   INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Impuestos
    SET Baja = 1
    WHERE ImpuestosID = @ImpuestosID
      AND EmpresaID   = @EmpresaID;
END;
