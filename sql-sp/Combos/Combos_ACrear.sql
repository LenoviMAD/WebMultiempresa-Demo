CREATE OR ALTER PROCEDURE [dbo].[Combos_ACrear]
    @EmpresaID              INT,
    @MarcasProductosID      INT,
    @Nombre                 NVARCHAR(150),
    @NombreAlternativo      NVARCHAR(150)  = NULL,
    @Codigo                 NVARCHAR(10),
    @Cantidad               INT            = 1,
    @CantidadPorFactura     DECIMAL(18,2)  = 1,
    @CantidadDinamica       DECIMAL(10,2)  = 0,
    @CantidadSinCargo       DECIMAL(7,2)   = 1,
    @ImporteProductosFuera  DECIMAL(18,2)  = NULL,
    @PorcentajeComision     DECIMAL(10,2)  = 0,
    @Nota                   NVARCHAR(MAX)  = NULL,
    @FechaInicio            DATETIME2,
    @FechaVigencia          DATETIME2,
    @TodosLosVendedores     BIT            = 1,
    @TodasLasListasPrecios  BIT            = 0,
    @TodasLasSucursales     BIT            = 1,
    @EsEstrategico          BIT            = 1,
    @SoloNoCompradores      BIT            = 0,
    @ComboDinamico          BIT            = 0,
    @EsDeIntroduccion       BIT            = 0,
    @NoImprimir             BIT            = 0,
    @ValidarPartido         BIT            = 1,
    @CupoTotal              INT            = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Combos] (
        [EmpresaID], [MarcasProductosID], [Nombre], [NombreAlternativo], [Codigo],
        [Cantidad], [CantidadPorFactura], [CantidadDinamica], [CantidadSinCargo],
        [ImporteProductosFuera], [PorcentajeComision], [Nota],
        [FechaInicio], [FechaVigencia],
        [TodosLosVendedores], [TodasLasListasPrecios], [TodasLasSucursales],
        [EsEstrategico], [SoloNoCompradores], [ComboDinamico],
        [EsDeIntroduccion], [NoImprimir], [ValidarPartido], [CupoTotal], [Baja]
    ) VALUES (
        @EmpresaID, @MarcasProductosID, @Nombre, @NombreAlternativo, @Codigo,
        @Cantidad, @CantidadPorFactura, @CantidadDinamica, @CantidadSinCargo,
        @ImporteProductosFuera, @PorcentajeComision, @Nota,
        @FechaInicio, @FechaVigencia,
        @TodosLosVendedores, @TodasLasListasPrecios, @TodasLasSucursales,
        @EsEstrategico, @SoloNoCompradores, @ComboDinamico,
        @EsDeIntroduccion, @NoImprimir, @ValidarPartido, @CupoTotal, 0
    );

    SELECT SCOPE_IDENTITY() AS [CombosID];
END
GO
