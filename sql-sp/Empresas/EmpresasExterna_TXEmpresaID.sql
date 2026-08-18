-- SP puente para ReglasDeDatos.GetFilter("EmpresaID", empresaId)
-- Devuelve la fila de Empresas mapeada al contrato EmpresasExternaItem
-- que espera el código legacy (EmpresasExternaID, EmpresaID, Nombre, KeyConexion, Baja)
CREATE OR ALTER PROCEDURE [dbo].[EmpresasExterna_TXEmpresaID]
    @Param1 INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        EmpresaID  AS EmpresasExternaID,
        EmpresaID,
        Nombre,
        KeyConexion,
        Baja
    FROM [dbo].[Empresas]
    WHERE EmpresaID = @Param1
      AND Baja = 0;
END
