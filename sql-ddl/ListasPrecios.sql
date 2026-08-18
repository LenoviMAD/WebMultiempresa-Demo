USE [WebMultiempresaDemo]
GO

CREATE TABLE [dbo].[ListasDePreciosMultiEmpresa] (
    [ListasDePreciosMultiEmpresaID] INT          IDENTITY(1,1) NOT NULL,
    [EmpresaID]                     INT          NOT NULL,
    [Nro]                           INT          NOT NULL,  -- correlaciona con ClientesMultiEmpresa.ListaDePrecio
                                                             -- y con PrecioUnitarioFinalL1..L8 de ProductosMultiEmpresa
    [Nombre]                        NVARCHAR(50) NOT NULL,
    [Baja]                          BIT          NOT NULL CONSTRAINT [DF_ListasDePreciosMultiEmpresa_Baja] DEFAULT 0,
    CONSTRAINT [PK_ListasDePreciosMultiEmpresa]
        PRIMARY KEY CLUSTERED ([ListasDePreciosMultiEmpresaID] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
              ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON),
    CONSTRAINT [UQ_ListasPrecios_EmpresaNro]
        UNIQUE NONCLUSTERED ([EmpresaID] ASC, [Nro] ASC)
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_ListasDePreciosMultiEmpresa_EmpresaID]
    ON [dbo].[ListasDePreciosMultiEmpresa] ([EmpresaID] ASC, [Baja] ASC)
GO
