CREATE TABLE [dbo].[UsuarioAppPermisos] (
    [UsuarioAppPermisosID] INT           NOT NULL IDENTITY(1,1),
    [UsuariosID]           INT           NOT NULL,
    [AplicacionesID]       INT           NOT NULL,
    [Permiso]              NVARCHAR(100) NOT NULL,
    [Baja]                 BIT           NOT NULL CONSTRAINT [DF_UsuarioAppPermisos_Baja] DEFAULT 0,
    CONSTRAINT [PK_UsuarioAppPermisos]       PRIMARY KEY ([UsuarioAppPermisosID]),
    CONSTRAINT [FK_UsuarioAppPermisos_Usr]   FOREIGN KEY ([UsuariosID])     REFERENCES [dbo].[Usuarios]     ([UsuariosID]) ON DELETE CASCADE,
    CONSTRAINT [FK_UsuarioAppPermisos_App]   FOREIGN KEY ([AplicacionesID]) REFERENCES [dbo].[Aplicaciones] ([AplicacionesID]),
    CONSTRAINT [UQ_UsuarioAppPermisos]       UNIQUE ([UsuariosID], [AplicacionesID], [Permiso])
);
