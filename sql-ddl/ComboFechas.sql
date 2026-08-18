USE [WebMultiempresaDemo]
GO

CREATE TABLE [dbo].[CombosFechasMultiEmpresa] (
    [CombosFechasMultiEmpresaID] INT           IDENTITY(1,1) NOT NULL,
    [CombosMultiEmpresaID]       INT           NOT NULL,
    [FechaInicial]               SMALLDATETIME NOT NULL,
    [FechaFinal]                 SMALLDATETIME NOT NULL,
    CONSTRAINT [PK_CombosFechasMultiEmpresa]
        PRIMARY KEY CLUSTERED ([CombosFechasMultiEmpresaID] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
              ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON),
    CONSTRAINT [FK_CombosFechas_Combo]
        FOREIGN KEY ([CombosMultiEmpresaID])
        REFERENCES [dbo].[CombosMultiEmpresa] ([CombosMultiEmpresaID])
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_CombosFechasMultiEmpresa_ComboID]
    ON [dbo].[CombosFechasMultiEmpresa] ([CombosMultiEmpresaID] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_CombosFechasMultiEmpresa_Fechas]
    ON [dbo].[CombosFechasMultiEmpresa] ([FechaInicial] ASC, [FechaFinal] ASC)
GO
