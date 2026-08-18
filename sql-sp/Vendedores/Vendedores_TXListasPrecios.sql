-- Lista de precios habilitadas para un vendedor. Usado en el login para
-- popular el selector de lista en la app (ListadDisponibles + lista default).
CREATE OR ALTER PROCEDURE Vendedores_TXListasPrecios
    @VendedoresID  INT,
    @EmpresaID     INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        lp.ListasPreciosID,
        lp.Nombre,
        vlp.EsDefault
    FROM VendedorListasPrecios vlp
    INNER JOIN ListasPrecios lp
        ON  lp.ListasPreciosID = vlp.ListasPreciosID
        AND lp.EmpresaID       = vlp.EmpresaID
    WHERE vlp.VendedoresID = @VendedoresID
      AND vlp.EmpresaID    = @EmpresaID
      AND vlp.Baja         = 0
      AND lp.Baja          = 0
    ORDER BY vlp.EsDefault DESC, lp.Nombre ASC;
END
