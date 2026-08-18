-- Inserta un punto de posición GPS de un vendedor.
-- Reemplaza INSERT INTO GpsLocations (legacy) para empresas con EmpresaID > 0.
-- OrigenEnvio se fija en 'VendedoresAPI' en la capa de aplicación.
CREATE OR ALTER PROCEDURE GpsPosicion_ARegistrar
    @EmpresaID         INT,
    @AplicacionesID    INT,
    @VendedoresID      INT,
    @Latitud           DECIMAL(10,7),
    @Longitud          DECIMAL(11,7),
    @FechaHora         DATETIME2,
    @VelocidadKmh      DECIMAL(5,1)   = NULL,
    @AccuracyMetros    INT            = NULL,
    @DistanciaMetros   DECIMAL(10,2)  = NULL,
    @ModeloDispositivo NVARCHAR(100)  = NULL,
    @NumeroDispositivo NVARCHAR(50)   = NULL,
    @OrigenEnvio       NVARCHAR(50)   = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO GpsPosiciones
    (
        EmpresaID, AplicacionesID, VendedoresID,
        Latitud, Longitud, FechaHora,
        VelocidadKmh, AccuracyMetros,
        DistanciaMetros, ModeloDispositivo, NumeroDispositivo, OrigenEnvio
    )
    VALUES
    (
        @EmpresaID, @AplicacionesID, @VendedoresID,
        @Latitud, @Longitud, @FechaHora,
        @VelocidadKmh, @AccuracyMetros,
        @DistanciaMetros, @ModeloDispositivo, @NumeroDispositivo, @OrigenEnvio
    );
END
