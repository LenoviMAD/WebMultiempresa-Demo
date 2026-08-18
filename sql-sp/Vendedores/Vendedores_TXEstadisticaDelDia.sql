-- Devuelve la estadística del día para un vendedor junto con el estado
-- y valor de cada estrella. EstrellasDelDia = COUNT de estrellas encendidas.
CREATE OR ALTER PROCEDURE Vendedores_TXEstadisticaDelDia
    @VendedoresID  INT,
    @EmpresaID     INT,
    @Fecha         DATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Resumen del día
    SELECT
        e.VendedorEstadisticasID,
        e.Fecha,
        e.CoeficienteComision,
        (SELECT COUNT(*)
         FROM VendedorEstrellasDiarias ed
         WHERE ed.VendedoresID = @VendedoresID
           AND ed.EmpresaID    = @EmpresaID
           AND ed.Fecha        = @Fecha
           AND ed.EstaEncendida = 1
           AND ed.Baja         = 0) AS EstrellasDelDia
    FROM VendedorEstadisticas e
    WHERE e.VendedoresID = @VendedoresID
      AND e.EmpresaID    = @EmpresaID
      AND e.Fecha        = @Fecha
      AND e.Baja         = 0;

    -- Detalle por estrella
    SELECT
        def.VendedorEstrellasDefinicionesID,
        def.NumeroEstrella,
        def.Nombre        AS NombreEstrella,
        def.ObjetivoMensual,
        ISNULL(ed.EstaEncendida, 0) AS EstaEncendida,
        ISNULL(ed.Valor, 0)         AS Valor
    FROM VendedorEstrellasDefiniciones def
    LEFT JOIN VendedorEstrellasDiarias ed
        ON  ed.VendedoresID                    = @VendedoresID
        AND ed.EmpresaID                       = @EmpresaID
        AND ed.Fecha                           = @Fecha
        AND ed.VendedorEstrellasDefinicionesID = def.VendedorEstrellasDefinicionesID
        AND ed.Baja                            = 0
    WHERE def.EmpresaID = @EmpresaID
      AND def.Baja      = 0
    ORDER BY def.NumeroEstrella;
END
