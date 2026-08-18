CREATE OR ALTER PROCEDURE Impuestos_ACrear
    @EmpresaID  INT,
    @Nombre     NVARCHAR(50),
    @Porcentaje DECIMAL(5,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Impuestos (EmpresaID, Nombre, Porcentaje, Baja)
    VALUES (@EmpresaID, @Nombre, @Porcentaje, 0);

    SELECT SCOPE_IDENTITY() AS ImpuestosID;
END;
