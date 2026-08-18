CREATE TABLE [dbo].[TiposActores] (
    [TiposActoresID]  INT           NOT NULL IDENTITY(1,1),
    [AplicacionesID]  INT           NOT NULL,
    [Nombre]          NVARCHAR(200) NOT NULL,
    [Codigo]          NVARCHAR(50)  NOT NULL,
    [Baja]            BIT           NOT NULL CONSTRAINT [DF_TiposActores_Baja] DEFAULT 0,
    CONSTRAINT [PK_TiposActores]         PRIMARY KEY ([TiposActoresID]),
    CONSTRAINT [FK_TiposActores_App]     FOREIGN KEY ([AplicacionesID]) REFERENCES [dbo].[Aplicaciones] ([AplicacionesID]),
    CONSTRAINT [UQ_TiposActores_Codigo]  UNIQUE ([AplicacionesID], [Codigo])
);
