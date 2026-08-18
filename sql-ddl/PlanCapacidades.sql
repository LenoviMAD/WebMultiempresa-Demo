CREATE TABLE [dbo].[PlanCapacidades] (
    [PlanCapacidadesID] INT NOT NULL IDENTITY(1,1),
    [PlanesID]          INT NOT NULL,
    [TiposActoresID]    INT NOT NULL,
    [MaxCapacidad]      INT NOT NULL,
    CONSTRAINT [PK_PlanCapacidades]          PRIMARY KEY ([PlanCapacidadesID]),
    CONSTRAINT [FK_PlanCapacidades_Plan]     FOREIGN KEY ([PlanesID])       REFERENCES [dbo].[Planes]       ([PlanesID]) ON DELETE CASCADE,
    CONSTRAINT [FK_PlanCapacidades_Tipo]     FOREIGN KEY ([TiposActoresID]) REFERENCES [dbo].[TiposActores] ([TiposActoresID]),
    CONSTRAINT [UQ_PlanCapacidades]          UNIQUE ([PlanesID], [TiposActoresID])
);
