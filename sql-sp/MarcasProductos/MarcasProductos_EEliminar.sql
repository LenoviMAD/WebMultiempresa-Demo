CREATE OR ALTER PROCEDURE [dbo].[MarcasProductos_EEliminar]
    @MarcasProductosID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Baja lógica — no eliminar físicamente para mantener integridad con combos
    UPDATE [dbo].[MarcasProductos]
    SET    [Baja] = 1
    WHERE  [MarcasProductosID] = @MarcasProductosID;
END
GO
