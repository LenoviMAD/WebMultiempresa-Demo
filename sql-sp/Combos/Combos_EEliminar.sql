CREATE OR ALTER PROCEDURE [dbo].[Combos_EEliminar]
    @CombosID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Baja lógica — no eliminar físicamente para preservar historial de pedidos
    UPDATE [dbo].[Combos]
    SET    [Baja] = 1
    WHERE  [CombosID] = @CombosID;
END
GO
