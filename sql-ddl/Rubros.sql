USE [WebMultiempresaDemo]
GO

CREATE TABLE [dbo].[RubrosMultiEmpresa] (
    [RubrosMultiEmpresaID] INT           IDENTITY(1,1) NOT NULL,
    [EmpresaID]            INT           NOT NULL,
    [Nombre]               NVARCHAR(100) NOT NULL,
    [Orden]                INT           NOT NULL CONSTRAINT [DF_RubrosMultiEmpresa_Orden] DEFAULT 0,
    [Baja]                 BIT           NOT NULL CONSTRAINT [DF_RubrosMultiEmpresa_Baja]  DEFAULT 0,
    CONSTRAINT [PK_RubrosMultiEmpresa]
        PRIMARY KEY CLUSTERED ([RubrosMultiEmpresaID] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
              ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_RubrosMultiEmpresa_EmpresaID]
    ON [dbo].[RubrosMultiEmpresa] ([EmpresaID] ASC, [Baja] ASC)
GO
