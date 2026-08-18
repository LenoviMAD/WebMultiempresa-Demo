-- Elimina todos los items de un combo (usado al reemplazar items en bloque)
CREATE OR ALTER PROCEDURE [dbo].[ComboItems_EEliminarPorCombo]
    @CombosID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[ComboItems]
    WHERE  [CombosID] = @CombosID;
END
GO
