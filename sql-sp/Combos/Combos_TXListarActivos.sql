CREATE OR ALTER PROCEDURE Combos_TXListarActivos
    @EmpresaID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.CombosID,
        c.Nombre,
        c.Codigo,
        ISNULL(r.Nombre, '') AS NombreMarcaProducto,
        c.FechaInicio,
        c.FechaVigencia,
        CAST(
            CASE
                WHEN c.FechaInicio <= GETUTCDATE() AND c.FechaVigencia >= GETUTCDATE()
                THEN 1
                ELSE 0
            END
        AS BIT) AS Vigente,
        c.Baja
    FROM  Combos  c
    LEFT JOIN MarcasProductos r ON r.MarcasProductosID = c.MarcasProductosID
    WHERE c.EmpresaID = @EmpresaID
      AND c.Baja = 0
    ORDER BY c.Nombre;
END
