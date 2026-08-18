CREATE TABLE [dbo].[ProductoPrecios] (
    [ProductoPreciosID] INT             NOT NULL IDENTITY(1,1),
    [ProductosID]       INT             NOT NULL,
    [ListasPreciosID]   INT             NOT NULL,
    [EmpresaID]         INT             NOT NULL,
    [PrecioFinal]       DECIMAL(18,4)   NOT NULL,
    [Baja]              BIT             NOT NULL CONSTRAINT [DF_ProductoPrecios_Baja] DEFAULT 0,
    CONSTRAINT [PK_ProductoPrecios]        PRIMARY KEY ([ProductoPreciosID]),
    -- ProductosID referencia tabla legacy Productos (sin FK constraint de BD)
    CONSTRAINT [FK_ProductoPrecios_Lista]  FOREIGN KEY ([ListasPreciosID]) REFERENCES [dbo].[ListasPrecios] ([ListasPreciosID]),
    CONSTRAINT [FK_ProductoPrecios_Emp]    FOREIGN KEY ([EmpresaID])       REFERENCES [dbo].[Empresas]      ([EmpresaID]),
    CONSTRAINT [UQ_ProductoPrecios]        UNIQUE ([ProductosID], [ListasPreciosID])
);
