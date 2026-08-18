CREATE OR ALTER PROCEDURE [dbo].[ListasPrecios_TXListarPorEmpresa]
    @EmpresaID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [ListasPreciosID],
           [EmpresaID],
           [Nombre],
           [PorcentajeMarcup],
           [Baja]
    FROM   [dbo].[ListasPrecios]
    WHERE  [EmpresaID] = @EmpresaID
      AND  [Baja] = 0
    ORDER BY [Nombre];
END
GO
