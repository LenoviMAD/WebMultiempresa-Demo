USE [WebMultiempresaDemo]
GO

-- Agrega FKs blandas de Rubro y Marca a ProductosMultiEmpresa.
-- Nullable para compatibilidad con registros existentes y proceso de sincronización
-- que puede cargar productos antes que rubros/marcas.
-- No se agregan FOREIGN KEY constraints para no bloquear la sincronización.
ALTER TABLE [dbo].[ProductosMultiEmpresa]
    ADD [RubrosMultiEmpresaID] INT NULL,
        [MarcasMultiEmpresaID] INT NULL
GO

CREATE NONCLUSTERED INDEX [IX_ProductosMultiEmpresa_RubroID]
    ON [dbo].[ProductosMultiEmpresa] ([EmpresaID] ASC, [RubrosMultiEmpresaID] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_ProductosMultiEmpresa_MarcaID]
    ON [dbo].[ProductosMultiEmpresa] ([EmpresaID] ASC, [MarcasMultiEmpresaID] ASC)
GO
