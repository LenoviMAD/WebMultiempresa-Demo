-- Lista la tabla de coeficientes de comisión según cantidad de estrellas para una empresa.
-- Ejemplo: 0 estrellas → 0.0, 4 estrellas → 1.0, 6 estrellas → 1.1
CREATE OR ALTER PROCEDURE Vendedores_TXEstrellasCoeficientes
    @EmpresaID  INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ec.CantidadEstrellas,
        ec.CoeficienteComision
    FROM VendedorEstrellasCoeficientes ec
    WHERE ec.EmpresaID = @EmpresaID
      AND ec.Baja      = 0
    ORDER BY ec.CantidadEstrellas;
END
