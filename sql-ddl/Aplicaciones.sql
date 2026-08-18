CREATE TABLE [dbo].[Aplicaciones] (
    [AplicacionesID] INT           NOT NULL IDENTITY(1,1),
    [Nombre]         NVARCHAR(200) NOT NULL,
    [Descripcion]    NVARCHAR(500) NULL,
    [Baja]           BIT           NOT NULL CONSTRAINT [DF_Aplicaciones_Baja] DEFAULT 0,
    CONSTRAINT [PK_Aplicaciones] PRIMARY KEY ([AplicacionesID])
);
