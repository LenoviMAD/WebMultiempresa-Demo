CREATE TABLE [dbo].[Planes] (
    [PlanesID]       INT           NOT NULL IDENTITY(1,1),
    [AplicacionesID] INT           NOT NULL,
    [Nombre]         NVARCHAR(200) NOT NULL,
    [Descripcion]    NVARCHAR(500) NULL,
    [Baja]           BIT           NOT NULL CONSTRAINT [DF_Planes_Baja] DEFAULT 0,
    CONSTRAINT [PK_Planes]      PRIMARY KEY ([PlanesID]),
    CONSTRAINT [FK_Planes_App]  FOREIGN KEY ([AplicacionesID]) REFERENCES [dbo].[Aplicaciones] ([AplicacionesID])
);
