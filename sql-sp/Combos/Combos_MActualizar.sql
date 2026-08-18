CREATE OR ALTER PROCEDURE [dbo].[Combos_MActualizar]
    @CombosID               INT,
    @MarcasProductosID      INT,
    @Nombre                 NVARCHAR(150),
    @NombreAlternativo      NVARCHAR(150)  = NULL,
    @Codigo                 NVARCHAR(10),
    @Cantidad               INT,
    @CantidadPorFactura     DECIMAL(18,2),
    @CantidadDinamica       DECIMAL(10,2),
    @CantidadSinCargo       DECIMAL(7,2),
    @ImporteProductosFuera  DECIMAL(18,2)  = NULL,
    @PorcentajeComision     DECIMAL(10,2),
    @Nota                   NVARCHAR(MAX)  = NULL,
    @FechaInicio            DATETIME2,
    @FechaVigencia          DATETIME2,
    @TodosLosVendedores     BIT,
    @TodasLasListasPrecios  BIT,
    @TodasLasSucursales     BIT,
    @EsEstrategico          BIT,
    @SoloNoCompradores      BIT,
    @ComboDinamico          BIT,
    @EsDeIntroduccion       BIT,
    @NoImprimir             BIT,
    @ValidarPartido         BIT,
    @CupoTotal              INT            = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Combos]
    SET    [MarcasProductosID]     = @MarcasProductosID,
           [Nombre]                = @Nombre,
           [NombreAlternativo]     = @NombreAlternativo,
           [Codigo]                = @Codigo,
           [Cantidad]              = @Cantidad,
           [CantidadPorFactura]    = @CantidadPorFactura,
           [CantidadDinamica]      = @CantidadDinamica,
           [CantidadSinCargo]      = @CantidadSinCargo,
           [ImporteProductosFuera] = @ImporteProductosFuera,
           [PorcentajeComision]    = @PorcentajeComision,
           [Nota]                  = @Nota,
           [FechaInicio]           = @FechaInicio,
           [FechaVigencia]         = @FechaVigencia,
           [TodosLosVendedores]    = @TodosLosVendedores,
           [TodasLasListasPrecios] = @TodasLasListasPrecios,
           [TodasLasSucursales]    = @TodasLasSucursales,
           [EsEstrategico]         = @EsEstrategico,
           [SoloNoCompradores]     = @SoloNoCompradores,
           [ComboDinamico]         = @ComboDinamico,
           [EsDeIntroduccion]      = @EsDeIntroduccion,
           [NoImprimir]            = @NoImprimir,
           [ValidarPartido]        = @ValidarPartido,
           [CupoTotal]             = @CupoTotal
    WHERE  [CombosID] = @CombosID;
END
GO
