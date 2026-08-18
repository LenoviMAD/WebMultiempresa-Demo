CREATE TABLE [dbo].[ClienteVendedores] (
    [ClienteVendedoresID] INT NOT NULL IDENTITY(1,1),
    [ClientesID]          INT NOT NULL,
    [VendedoresID]        INT NOT NULL,
    [EmpresaID]           INT NOT NULL,
    [Baja]                BIT NOT NULL CONSTRAINT [DF_ClienteVendedores_Baja] DEFAULT 0,
    CONSTRAINT [PK_ClienteVendedores]      PRIMARY KEY ([ClienteVendedoresID]),
    -- ClientesID y VendedoresID referencian tablas legacy (sin FK constraint de BD)
    CONSTRAINT [FK_ClienteVendedores_Emp]  FOREIGN KEY ([EmpresaID]) REFERENCES [dbo].[Empresas] ([EmpresaID]),
    CONSTRAINT [UQ_ClienteVendedores]      UNIQUE ([ClientesID], [VendedoresID])
);
