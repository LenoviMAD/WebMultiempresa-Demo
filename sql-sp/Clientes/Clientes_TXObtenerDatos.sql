-- Datos completos de un cliente por su ID.
-- Usado por el endpoint GET /clienteitem/getdatoscliente/{clientesID}/{empresaID}.
-- Nota: la BD nueva usa Documento + TiposDocumentosID en lugar de columnas Dni/Cuit separadas.
CREATE OR ALTER PROCEDURE Clientes_TXObtenerDatos
    @ClientesID  INT,
    @EmpresaID   INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.ClientesID                                                        AS cli_clientesid,
        c.EmpresaID                                                         AS empresaID,
        c.Codigo                                                            AS cli_codigo,
        c.Nombre                                                            AS cli_nombre,
        ISNULL(c.Direccion,  '')                                            AS cli_direccion,
        ISNULL(c.Localidad,  '')                                            AS cli_localidad,
        ISNULL(c.Provincia,  '')                                            AS provincia,
        ISNULL(c.CodigoPostal, '')                                          AS CodigoPostal,
        ISNULL(c.Telefono,   '')                                            AS cli_telefono,
        ISNULL(c.Email,      '')                                            AS cli_email,
        CASE WHEN c.TiposDocumentosID = 1 THEN ISNULL(c.Documento, '') ELSE '' END AS cli_DNI,
        CASE WHEN c.TiposDocumentosID <> 1 THEN ISNULL(c.Documento, '') ELSE '' END AS cli_CUIT,
        c.DiaDeVenta                                                        AS cli_diadeventa,
        0                                                                   AS cli_tipoclientesid,
        c.ConsumidorFinal,
        c.PorcentajePercepcionIB                                            AS cli_porcentajepercepcionib,
        ISNULL(c.Latitud,  0)                                               AS cli_latitud,
        ISNULL(c.Longitud, 0)                                               AS cli_longitud,
        ISNULL(lp.ListasPreciosID, 0)                                       AS cli_listadeprecio
    FROM Clientes c
    LEFT JOIN ClienteListasPrecios lp
        ON  lp.ClientesID  = c.ClientesID
        AND lp.EmpresaID   = @EmpresaID
        AND lp.EsPrincipal = 1
        AND lp.Baja        = 0
    WHERE c.ClientesID = @ClientesID
      AND c.EmpresaID  = @EmpresaID
      AND c.Baja       = 0;
END
