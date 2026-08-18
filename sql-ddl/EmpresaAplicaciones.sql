CREATE TABLE [dbo].[EmpresaAplicaciones] (
    [EmpresaAplicacionesID] INT       NOT NULL IDENTITY(1,1),
    [EmpresaID]             INT       NOT NULL,
    [AplicacionesID]        INT       NOT NULL,
    [FechaActivacion]       DATETIME2 NOT NULL,
    [FechaVencimiento]      DATETIME2 NULL,
    [Baja]                  BIT       NOT NULL CONSTRAINT [DF_EmpresaAplicaciones_Baja] DEFAULT 0,
    CONSTRAINT [PK_EmpresaAplicaciones]      PRIMARY KEY ([EmpresaAplicacionesID]),
    CONSTRAINT [FK_EmpresaAplicaciones_Emp]  FOREIGN KEY ([EmpresaID])      REFERENCES [dbo].[Empresas]      ([EmpresaID]),
    CONSTRAINT [FK_EmpresaAplicaciones_App]  FOREIGN KEY ([AplicacionesID]) REFERENCES [dbo].[Aplicaciones]  ([AplicacionesID]),
    CONSTRAINT [UQ_EmpresaAplicaciones]      UNIQUE ([EmpresaID], [AplicacionesID])
);
