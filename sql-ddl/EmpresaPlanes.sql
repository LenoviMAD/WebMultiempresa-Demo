CREATE TABLE [dbo].[EmpresaPlanes] (
    [EmpresaPlanesID]  INT       NOT NULL IDENTITY(1,1),
    [EmpresaID]        INT       NOT NULL,
    [PlanesID]         INT       NOT NULL,
    [FechaInicio]      DATETIME2 NOT NULL,
    [FechaVencimiento] DATETIME2 NULL,
    [Baja]             BIT       NOT NULL CONSTRAINT [DF_EmpresaPlanes_Baja] DEFAULT 0,
    CONSTRAINT [PK_EmpresaPlanes]      PRIMARY KEY ([EmpresaPlanesID]),
    CONSTRAINT [FK_EmpresaPlanes_Emp]  FOREIGN KEY ([EmpresaID]) REFERENCES [dbo].[Empresas] ([EmpresaID]),
    CONSTRAINT [FK_EmpresaPlanes_Plan] FOREIGN KEY ([PlanesID])  REFERENCES [dbo].[Planes]   ([PlanesID])
);
