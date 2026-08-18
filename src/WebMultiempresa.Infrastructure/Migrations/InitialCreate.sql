IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE TABLE [EmpresasMultiempresa] (
        [EmpresaID] int NOT NULL IDENTITY,
        [Nombre] nvarchar(500) NOT NULL,
        [KeyConexion] nvarchar(50) NOT NULL,
        [Baja] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_EmpresasMultiempresa] PRIMARY KEY ([EmpresaID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE TABLE [ListasPreciosMultiempresa] (
        [ListasPreciosID] int NOT NULL IDENTITY,
        [EmpresaID] int NOT NULL,
        [Nombre] nvarchar(100) NOT NULL,
        [Baja] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_ListasPreciosMultiempresa] PRIMARY KEY ([ListasPreciosID]),
        CONSTRAINT [FK_ListasPreciosMultiempresa_EmpresasMultiempresa_EmpresaID] FOREIGN KEY ([EmpresaID]) REFERENCES [EmpresasMultiempresa] ([EmpresaID]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE TABLE [MarcasMultiempresa] (
        [MarcasID] int NOT NULL IDENTITY,
        [EmpresaID] int NOT NULL,
        [Nombre] nvarchar(100) NOT NULL,
        [Baja] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_MarcasMultiempresa] PRIMARY KEY ([MarcasID]),
        CONSTRAINT [FK_MarcasMultiempresa_EmpresasMultiempresa_EmpresaID] FOREIGN KEY ([EmpresaID]) REFERENCES [EmpresasMultiempresa] ([EmpresaID]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE TABLE [RubrosMultiempresa] (
        [RubrosID] int NOT NULL IDENTITY,
        [EmpresaID] int NOT NULL,
        [Nombre] nvarchar(150) NOT NULL,
        [Baja] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_RubrosMultiempresa] PRIMARY KEY ([RubrosID]),
        CONSTRAINT [FK_RubrosMultiempresa_EmpresasMultiempresa_EmpresaID] FOREIGN KEY ([EmpresaID]) REFERENCES [EmpresasMultiempresa] ([EmpresaID]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE TABLE [SucursalesMultiempresa] (
        [SucursalesID] int NOT NULL IDENTITY,
        [EmpresaID] int NOT NULL,
        [Nombre] nvarchar(200) NOT NULL,
        [Baja] bit NOT NULL DEFAULT CAST(0 AS bit),
        CONSTRAINT [PK_SucursalesMultiempresa] PRIMARY KEY ([SucursalesID]),
        CONSTRAINT [FK_SucursalesMultiempresa_EmpresasMultiempresa_EmpresaID] FOREIGN KEY ([EmpresaID]) REFERENCES [EmpresasMultiempresa] ([EmpresaID]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE TABLE [UsuariosMultiempresa] (
        [UsuariosID] int NOT NULL IDENTITY,
        [EmpresaID] int NULL,
        [Email] nvarchar(200) NOT NULL,
        [PasswordHash] nvarchar(256) NOT NULL,
        [Nombre] nvarchar(200) NOT NULL,
        [Rol] tinyint NOT NULL,
        [Baja] bit NOT NULL DEFAULT CAST(0 AS bit),
        [FechaCreacion] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        CONSTRAINT [PK_UsuariosMultiempresa] PRIMARY KEY ([UsuariosID]),
        CONSTRAINT [FK_UsuariosMultiempresa_EmpresasMultiempresa_EmpresaID] FOREIGN KEY ([EmpresaID]) REFERENCES [EmpresasMultiempresa] ([EmpresaID])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE TABLE [CombosMultiempresa] (
        [CombosID] int NOT NULL IDENTITY,
        [EmpresaID] int NOT NULL,
        [RubrosID] int NOT NULL,
        [Nombre] nvarchar(150) NOT NULL,
        [NombreAlternativo] nvarchar(150) NULL,
        [Codigo] nvarchar(10) NOT NULL,
        [Cantidad] int NOT NULL,
        [CantidadFacturada] int NOT NULL,
        [CantidadPorFactura] decimal(18,2) NOT NULL,
        [CantidadDinamica] decimal(10,2) NOT NULL,
        [CantidadDinamicaMaxima] int NOT NULL,
        [CantidadSinCargo] decimal(7,2) NOT NULL DEFAULT 1.0,
        [ImporteProductosFuera] decimal(18,2) NULL,
        [PorcentajeComision] decimal(10,2) NOT NULL,
        [Nota] nvarchar(max) NULL,
        [FechaInicio] datetime2 NOT NULL,
        [FechaVigencia] datetime2 NOT NULL,
        [TodosLosVendedores] bit NOT NULL DEFAULT CAST(1 AS bit),
        [TodasLasListasPrecios] bit NOT NULL,
        [TodasLasSucursales] bit NOT NULL DEFAULT CAST(1 AS bit),
        [ClientesNumericos] bit NOT NULL,
        [ClientesAlfaNumericos] bit NOT NULL,
        [EsEstrategico] bit NOT NULL DEFAULT CAST(1 AS bit),
        [SoloNoCompradores] bit NOT NULL,
        [SoloNoCompradoresDesde] datetime2 NULL,
        [ComboDinamico] bit NOT NULL,
        [EsDeIntroduccion] bit NOT NULL,
        [NoImprimir] bit NOT NULL,
        [ImpresionResumida] bit NOT NULL,
        [UsarDescuentoClientes] bit NOT NULL,
        [ValidarPartido] bit NOT NULL DEFAULT CAST(1 AS bit),
        [CupoTotal] int NULL,
        [Baja] bit NOT NULL DEFAULT CAST(0 AS bit),
        [FechaCreacion] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        CONSTRAINT [PK_CombosMultiempresa] PRIMARY KEY ([CombosID]),
        CONSTRAINT [FK_CombosMultiempresa_EmpresasMultiempresa_EmpresaID] FOREIGN KEY ([EmpresaID]) REFERENCES [EmpresasMultiempresa] ([EmpresaID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CombosMultiempresa_RubrosMultiempresa_RubrosID] FOREIGN KEY ([RubrosID]) REFERENCES [RubrosMultiempresa] ([RubrosID]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE TABLE [ComboFechasMultiempresa] (
        [ComboFechasID] int NOT NULL IDENTITY,
        [CombosID] int NOT NULL,
        [FechaInicial] datetime2 NOT NULL,
        [FechaFinal] datetime2 NOT NULL,
        CONSTRAINT [PK_ComboFechasMultiempresa] PRIMARY KEY ([ComboFechasID]),
        CONSTRAINT [FK_ComboFechasMultiempresa_CombosMultiempresa_CombosID] FOREIGN KEY ([CombosID]) REFERENCES [CombosMultiempresa] ([CombosID]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE TABLE [ComboItemsMultiempresa] (
        [ComboItemsID] int NOT NULL IDENTITY,
        [CombosID] int NOT NULL,
        [ProductosID] int NOT NULL,
        [Cantidad] decimal(18,4) NOT NULL,
        [Precio] decimal(18,4) NOT NULL,
        [Tipo] tinyint NOT NULL,
        [Descuento1] decimal(10,2) NOT NULL,
        [Descuento2] decimal(10,2) NOT NULL,
        [NroGrupoDinamico] int NOT NULL,
        CONSTRAINT [PK_ComboItemsMultiempresa] PRIMARY KEY ([ComboItemsID]),
        CONSTRAINT [FK_ComboItemsMultiempresa_CombosMultiempresa_CombosID] FOREIGN KEY ([CombosID]) REFERENCES [CombosMultiempresa] ([CombosID]) ON DELETE CASCADE,
        CONSTRAINT [FK_ComboItemsMultiempresa_ProductosMultiEmpresa_ProductosID] FOREIGN KEY ([ProductosID]) REFERENCES [ProductosMultiEmpresa] ([ProductosMultiEmpresaID]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ComboFechasMultiempresa_CombosID] ON [ComboFechasMultiempresa] ([CombosID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ComboItemsMultiempresa_CombosID] ON [ComboItemsMultiempresa] ([CombosID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ComboItemsMultiempresa_ProductosID] ON [ComboItemsMultiempresa] ([ProductosID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_CombosMultiempresa_EmpresaID_Codigo] ON [CombosMultiempresa] ([EmpresaID], [Codigo]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_CombosMultiempresa_RubrosID] ON [CombosMultiempresa] ([RubrosID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_EmpresasMultiempresa_KeyConexion] ON [EmpresasMultiempresa] ([KeyConexion]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_ListasPreciosMultiempresa_EmpresaID] ON [ListasPreciosMultiempresa] ([EmpresaID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_MarcasMultiempresa_EmpresaID] ON [MarcasMultiempresa] ([EmpresaID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_RubrosMultiempresa_EmpresaID] ON [RubrosMultiempresa] ([EmpresaID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_SucursalesMultiempresa_EmpresaID] ON [SucursalesMultiempresa] ([EmpresaID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_UsuariosMultiempresa_Email] ON [UsuariosMultiempresa] ([Email]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_UsuariosMultiempresa_EmpresaID] ON [UsuariosMultiempresa] ([EmpresaID]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260506160938_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260506160938_InitialCreate', N'9.0.0');
END;

COMMIT;
GO

