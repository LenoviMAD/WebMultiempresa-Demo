-- Devuelve el estado de estrellas del último día con registros para un vendedor.
-- Si no hay historial, devuelve todas las definiciones con EstaEncendida = 0.
CREATE OR ALTER PROCEDURE Vendedores_TXEstrellasDiariasUltimas
    @VendedoresID  INT,
    @EmpresaID     INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UltimaFecha DATE;
    SELECT TOP 1 @UltimaFecha = Fecha
    FROM VendedorEstrellasDiarias
    WHERE VendedoresID = @VendedoresID
      AND EmpresaID    = @EmpresaID
      AND Baja         = 0
    ORDER BY Fecha DESC;

    IF @UltimaFecha IS NULL
    BEGIN
        SELECT
            ed.NumeroEstrella,
            CAST(0 AS BIT)  AS EstaEncendida,
            CAST(0 AS DECIMAL(18,4)) AS Valor
        FROM VendedorEstrellasDefiniciones ed
        WHERE ed.EmpresaID = @EmpresaID
          AND ed.Baja      = 0
        ORDER BY ed.NumeroEstrella;
        RETURN;
    END

    SELECT
        def.NumeroEstrella,
        ISNULL(esd.EstaEncendida, 0) AS EstaEncendida,
        ISNULL(esd.Valor, 0)         AS Valor
    FROM VendedorEstrellasDefiniciones def
    LEFT JOIN VendedorEstrellasDiarias esd
        ON  esd.VendedoresID                    = @VendedoresID
        AND esd.EmpresaID                       = @EmpresaID
        AND esd.Fecha                           = @UltimaFecha
        AND esd.VendedorEstrellasDefinicionesID = def.VendedorEstrellasDefinicionesID
        AND esd.Baja                            = 0
    WHERE def.EmpresaID = @EmpresaID
      AND def.Baja      = 0
    ORDER BY def.NumeroEstrella;
END
