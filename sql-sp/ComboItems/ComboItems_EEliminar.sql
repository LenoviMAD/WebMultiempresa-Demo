CREATE OR ALTER PROCEDURE [dbo].[ComboItems_EEliminar]
    @ComboItemsID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[ComboItems]
    WHERE  [ComboItemsID] = @ComboItemsID;
END
GO
