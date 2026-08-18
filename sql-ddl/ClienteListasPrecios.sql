CREATE TABLE [dbo].[ClienteListasPrecios] (
    [ClienteListasPreciosID] INT NOT NULL IDENTITY(1,1),
    [ClientesID]             INT NOT NULL,
    [ListasPreciosID]        INT NOT NULL,
    [EmpresaID]              INT NOT NULL,
    [EsPrincipal]            BIT NOT NULL CONSTRAINT [DF_ClienteListasPrecios_EsPrincipal] DEFAULT 0,
    [Baja]                   BIT NOT NULL CONSTRAINT [DF_ClienteListasPrecios_Baja]        DEFAULT 0,
    CONSTRAINT [PK_ClienteListasPrecios]        PRIMARY KEY ([ClienteListasPreciosID]),
    -- ClientesID referencia tabla legacy (sin FK constraint de BD)
    CONSTRAINT [FK_ClienteListasPrecios_Lista]  FOREIGN KEY ([ListasPreciosID]) REFERENCES [dbo].[ListasPrecios] ([ListasPreciosID]),
    CONSTRAINT [FK_ClienteListasPrecios_Emp]    FOREIGN KEY ([EmpresaID])       REFERENCES [dbo].[Empresas]      ([EmpresaID]),
    CONSTRAINT [UQ_ClienteListasPrecios]        UNIQUE ([ClientesID], [ListasPreciosID])
);
