USE [WebMultiempresaDemo]
GO

CREATE TABLE [dbo].[CombosMultiEmpresa] (
    [CombosMultiEmpresaID]         INT           IDENTITY(1,1) NOT NULL,
    [EmpresaID]                    INT           NOT NULL,
    [Nombre]                       NVARCHAR(150) NOT NULL,
    [Codigo]                       VARCHAR(10)   NOT NULL,
    [RubrosMultiEmpresaID]         INT           NOT NULL,
    [FechaInicio]                  SMALLDATETIME NOT NULL,
    [FechaDeVigencia]              SMALLDATETIME NOT NULL,
    [Cantidad]                     INT           NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_Cantidad]   DEFAULT 1,
    [CantidadPorFactura]           DECIMAL(18,2) NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_CantXFact]  DEFAULT 1,
    [CantidadMaximaPorCliente]     INT           NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_CantMaxCli] DEFAULT 0,
    [CantidadSinCargo]             DECIMAL(7,2)  NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_CantSCargo] DEFAULT 0,
    [CantidadDinamica]             DECIMAL(10,2) NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_CantDin]    DEFAULT 0,
    [Grupo1]   INT NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_G1]  DEFAULT 0,
    [Grupo2]   INT NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_G2]  DEFAULT 0,
    [Grupo3]   INT NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_G3]  DEFAULT 0,
    [Grupo4]   INT NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_G4]  DEFAULT 0,
    [Grupo5]   INT NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_G5]  DEFAULT 0,
    [Grupo6]   INT NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_G6]  DEFAULT 0,
    [Grupo7]   INT NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_G7]  DEFAULT 0,
    [Grupo8]   INT NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_G8]  DEFAULT 0,
    [Grupo9]   INT NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_G9]  DEFAULT 0,
    [Grupo10]  INT NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_G10] DEFAULT 0,
    [ImporteProductosFueraDeCombo] DECIMAL(18,2) NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_Arrastr]  DEFAULT 0,
    [SumarProductosDeCombos]       BIT           NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_SumProd]  DEFAULT 0,
    [EsIntroduccion]               BIT           NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_Intro]    DEFAULT 0,
    [EsEstrategico]                BIT           NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_Estrateg] DEFAULT 0,
    [SoloNoCompradores]            BIT           NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_SoloNC]   DEFAULT 0,
    [Baja]                         BIT           NOT NULL CONSTRAINT [DF_CombosMultiEmpresa_Baja]     DEFAULT 0,
    CONSTRAINT [PK_CombosMultiEmpresa]
        PRIMARY KEY CLUSTERED ([CombosMultiEmpresaID] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
              ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON),
    CONSTRAINT [UQ_CombosMultiEmpresa_CodigoEmpresa]
        UNIQUE NONCLUSTERED ([EmpresaID] ASC, [Codigo] ASC),
    CONSTRAINT [FK_CombosMultiEmpresa_Rubros]
        FOREIGN KEY ([RubrosMultiEmpresaID])
        REFERENCES [dbo].[RubrosMultiEmpresa] ([RubrosMultiEmpresaID])
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_CombosMultiEmpresa_EmpresaID]
    ON [dbo].[CombosMultiEmpresa] ([EmpresaID] ASC, [Baja] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_CombosMultiEmpresa_Vigencia]
    ON [dbo].[CombosMultiEmpresa] ([EmpresaID] ASC, [FechaInicio] ASC, [FechaDeVigencia] ASC, [Baja] ASC)
GO
