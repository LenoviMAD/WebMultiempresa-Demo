CREATE OR ALTER PROCEDURE [dbo].[ComboItems_ACrear]
    @CombosID        INT,
    @ProductosID     INT,
    @Cantidad        DECIMAL(18,4) = 1,
    @Tipo            TINYINT       = 1,
    @Precio          DECIMAL(18,4) = 0,
    @Descuento1      DECIMAL(10,2) = 0,
    @Descuento2      DECIMAL(10,2) = 0,
    @NroGrupoDinamico INT          = 0
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[ComboItems] (
        [CombosID], [ProductosID], [Cantidad], [Tipo], [Precio],
        [Descuento1], [Descuento2], [NroGrupoDinamico]
    ) VALUES (
        @CombosID, @ProductosID, @Cantidad, @Tipo, @Precio,
        @Descuento1, @Descuento2, @NroGrupoDinamico
    );

    SELECT SCOPE_IDENTITY() AS [ComboItemsID];
END
GO
