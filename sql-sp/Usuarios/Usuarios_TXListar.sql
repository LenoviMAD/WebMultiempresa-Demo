CREATE OR ALTER PROCEDURE Usuarios_TXListar
    @EmpresaID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.UsuariosID,
        u.EmpresaID,
        ISNULL(e.Nombre, '') AS NombreEmpresa,
        u.Email,
        u.Nombre,
        u.Rol,
        u.Baja,
        u.FechaCreacion
    FROM  Usuarios  u
    LEFT JOIN Empresas e ON e.EmpresaID = u.EmpresaID
    WHERE u.Baja = 0
      AND (@EmpresaID IS NULL OR u.EmpresaID = @EmpresaID)
    ORDER BY u.Nombre;
END
