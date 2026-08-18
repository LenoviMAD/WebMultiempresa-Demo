CREATE OR ALTER PROCEDURE Impuestos_MActualizar
    @ImpuestosID INT,
    @EmpresaID   INT,
    @Nombre      NVARCHAR(50),
    @Porcentaje  DECIMAL(5,2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Impuestos
    SET Nombre     = @Nombre,
        Porcentaje = @Porcentaje
    WHERE ImpuestosID = @ImpuestosID
      AND EmpresaID   = @EmpresaID
      AND Baja = 0;
END;
