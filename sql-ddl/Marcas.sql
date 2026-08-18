USE [WebMultiempresaDemo]
GO

CREATE TABLE [dbo].[MarcasMultiEmpresa] (
    [MarcasMultiEmpresaID] INT           IDENTITY(1,1) NOT NULL,
    [EmpresaID]            INT           NOT NULL,
    [Nombre]               NVARCHAR(100) NOT NULL,
    [Baja]                 BIT           NOT NULL CONSTRAINT [DF_MarcasMultiEmpresa_Baja] DEFAULT 0,
    CONSTRAINT [PK_MarcasMultiEmpresa]
        PRIMARY KEY CLUSTERED ([MarcasMultiEmpresaID] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
              ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_MarcasMultiEmpresa_EmpresaID]
    ON [dbo].[MarcasMultiEmpresa] ([EmpresaID] ASC, [Baja] ASC)
GO
