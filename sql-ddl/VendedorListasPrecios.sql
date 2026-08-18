CREATE TABLE [dbo].[VendedorListasPrecios] (
    [VendedorListasPreciosID] INT  NOT NULL IDENTITY(1,1),
    [VendedoresID]            INT  NOT NULL,
    [ListasPreciosID]         INT  NOT NULL,
    [EsDefault]               BIT  NOT NULL CONSTRAINT [DF_VendedorListasPrecios_EsDefault] DEFAULT 0,
    [EmpresaID]               INT  NOT NULL,
    [Baja]                    BIT  NOT NULL CONSTRAINT [DF_VendedorListasPrecios_Baja]      DEFAULT 0,
    CONSTRAINT [PK_VendedorListasPrecios]          PRIMARY KEY ([VendedorListasPreciosID]),
    CONSTRAINT [FK_VendedorListasPrecios_Vendedor] FOREIGN KEY ([VendedoresID])    REFERENCES [dbo].[Vendedores]    ([VendedoresID]),
    CONSTRAINT [FK_VendedorListasPrecios_Lista]    FOREIGN KEY ([ListasPreciosID]) REFERENCES [dbo].[ListasPrecios] ([ListasPreciosID])
);
