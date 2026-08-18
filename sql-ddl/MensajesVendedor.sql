-- Mensajes enviados a vendedores desde el portal (equivalente a MensajeAVendedor del ERP legacy).
-- Soporte para notificaciones WhatsApp y alertas de bloqueo.
-- Por ahora no se usa activamente; esquema listo para implementación futura.
CREATE TABLE MensajesVendedor (
    MensajesVendedorID BIGINT        IDENTITY(1,1) NOT NULL,
    EmpresaID          INT           NOT NULL,
    VendedoresID       INT           NOT NULL,
    Bloqueado          BIT           NOT NULL DEFAULT 0,
    Subject            NVARCHAR(300) NULL,
    Mensaje            NVARCHAR(MAX) NULL,
    Leido              BIT           NOT NULL DEFAULT 0,
    FechaCarga         DATETIME2     NOT NULL DEFAULT GETUTCDATE(),
    Baja               BIT           NOT NULL DEFAULT 0,

    CONSTRAINT PK_MensajesVendedor PRIMARY KEY (MensajesVendedorID),
    CONSTRAINT FK_MensajesVendedor_Empresas   FOREIGN KEY (EmpresaID)    REFERENCES Empresas   (EmpresaID),
    CONSTRAINT FK_MensajesVendedor_Vendedores FOREIGN KEY (VendedoresID) REFERENCES Vendedores (VendedoresID)
);

CREATE INDEX IX_MensajesVendedor_Vendedor
    ON MensajesVendedor (EmpresaID, VendedoresID, Leido, Baja);
