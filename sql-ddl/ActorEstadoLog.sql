CREATE TABLE [dbo].[ActorEstadoLog] (
    [ActorEstadoLogID] BIGINT        NOT NULL IDENTITY(1,1),
    [TiposActoresID]   INT           NOT NULL,
    [ActorID]          INT           NOT NULL,
    [EmpresaID]        INT           NOT NULL,
    [TipoEvento]       VARCHAR(10)   NOT NULL,
    [FechaEvento]      DATETIME2     NOT NULL,
    CONSTRAINT [PK_ActorEstadoLog]      PRIMARY KEY ([ActorEstadoLogID]),
    CONSTRAINT [FK_ActorEstadoLog_Tipo] FOREIGN KEY ([TiposActoresID]) REFERENCES [dbo].[TiposActores] ([TiposActoresID]),
    CONSTRAINT [CK_ActorEstadoLog_TipoEvento] CHECK ([TipoEvento] IN ('Alta', 'Baja'))
);

CREATE INDEX [IX_ActorEstadoLog_Cobro]
    ON [dbo].[ActorEstadoLog] ([TiposActoresID], [EmpresaID], [TipoEvento], [FechaEvento])
    INCLUDE ([ActorID]);
