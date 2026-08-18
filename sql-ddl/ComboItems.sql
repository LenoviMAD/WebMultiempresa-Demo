USE [WebMultiempresaDemo]
GO

CREATE TABLE [dbo].[CombosProductosMultiEmpresa] (
    [CombosProductosMultiEmpresaID] INT           IDENTITY(1,1) NOT NULL,
    [CombosMultiEmpresaID]          INT           NOT NULL,
    [ProductosMultiEmpresaID]       INT           NOT NULL,
    [Cantidad]                      INT           NOT NULL CONSTRAINT [DF_CombosProductos_Cant]  DEFAULT 1,
    [Tipo]                          INT           NOT NULL CONSTRAINT [DF_CombosProductos_Tipo]  DEFAULT 1,  -- 1=con cargo, 0=sin cargo
    [Descuento1]                    DECIMAL(10,4) NOT NULL CONSTRAINT [DF_CombosProductos_Desc1] DEFAULT 0,
    [Descuento2]                    DECIMAL(10,4) NOT NULL CONSTRAINT [DF_CombosProductos_Desc2] DEFAULT 0,
    [NrGrupoDinamico]               INT           NOT NULL CONSTRAINT [DF_CombosProductos_NrGrp] DEFAULT 0,
    [Orden]                         INT           NOT NULL CONSTRAINT [DF_CombosProductos_Orden] DEFAULT 0,
    CONSTRAINT [PK_CombosProductosMultiEmpresa]
        PRIMARY KEY CLUSTERED ([CombosProductosMultiEmpresaID] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
              ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON),
    CONSTRAINT [FK_CombosProductos_Combo]
        FOREIGN KEY ([CombosMultiEmpresaID])
        REFERENCES [dbo].[CombosMultiEmpresa] ([CombosMultiEmpresaID]),
    CONSTRAINT [FK_CombosProductos_Producto]
        FOREIGN KEY ([ProductosMultiEmpresaID])
        REFERENCES [dbo].[ProductosMultiEmpresa] ([ProductosMultiEmpresaID])
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_CombosProductosMultiEmpresa_ComboID]
    ON [dbo].[CombosProductosMultiEmpresa] ([CombosMultiEmpresaID] ASC, [Tipo] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_CombosProductosMultiEmpresa_ProductoID]
    ON [dbo].[CombosProductosMultiEmpresa] ([ProductosMultiEmpresaID] ASC)
GO
