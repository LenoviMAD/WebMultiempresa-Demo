-- Tabla de alto volumen: BIGINT PK, sin columna Baja, particionado mensual recomendado
-- Renombrada de GPS_Posiciones a GpsPosiciones (migración RenombrarTablaGpsPosiciones)
CREATE TABLE [dbo].[GpsPosiciones] (
    [GpsPosicionesID]   BIGINT         NOT NULL IDENTITY(1,1),
    [EmpresaID]         INT            NOT NULL,
    [AplicacionesID]    INT            NOT NULL,
    [VendedoresID]      INT            NOT NULL,
    [Latitud]           DECIMAL(10,7)  NOT NULL,
    [Longitud]          DECIMAL(11,7)  NOT NULL,
    [VelocidadKmh]      DECIMAL(5,1)   NULL,
    [AccuracyMetros]    INT            NULL,
    [DistanciaMetros]   DECIMAL(10,2)  NULL,
    [ModeloDispositivo] NVARCHAR(100)  NULL,
    [NumeroDispositivo] NVARCHAR(50)   NULL,
    [OrigenEnvio]       NVARCHAR(50)   NULL,
    [FechaHora]         DATETIME2      NOT NULL,
    CONSTRAINT [PK_GpsPosiciones]                PRIMARY KEY ([GpsPosicionesID]),
    CONSTRAINT [FK_GpsPosiciones_Aplicaciones]   FOREIGN KEY ([AplicacionesID]) REFERENCES [dbo].[Aplicaciones] ([AplicacionesID]),
    CONSTRAINT [FK_GpsPosiciones_Vendedores]     FOREIGN KEY ([VendedoresID])   REFERENCES [dbo].[Vendedores]   ([VendedoresID])
);
GO

CREATE INDEX [IX_GpsPosiciones_EmpresaID_VendedoresID_FechaHora]
    ON [dbo].[GpsPosiciones] ([EmpresaID], [VendedoresID], [FechaHora]);
GO
