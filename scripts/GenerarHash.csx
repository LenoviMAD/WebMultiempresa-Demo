#!/usr/bin/env dotnet-script
// =====================================================================
// GenerarHash.csx — genera un hash BCrypt para usar en el seed
// =====================================================================
// REQUISITOS:
//   dotnet tool install -g dotnet-script
//
// USO:
//   dotnet script scripts\GenerarHash.csx -- "Test123!" 11
//   dotnet script scripts\GenerarHash.csx -- "MiClave123"
//
// La salida es el hash listo para copiar en:
//   EXEC Vendedores_MActualizarClave @VendedoresID=1, @EmpresaID=1, @ClaveHash='<hash>';
// =====================================================================

#r "nuget: BCrypt.Net-Next, 4.0.3"

using BCrypt.Net;

string password   = Args.Count > 0 ? Args[0] : "Test123!";
int    workFactor = Args.Count > 1 && int.TryParse(Args[1], out int wf) ? wf : 11;

string hash = BCrypt.Net.BCrypt.HashPassword(password, workFactor);

Console.WriteLine();
Console.WriteLine($"Contraseña : {password}");
Console.WriteLine($"Work factor: {workFactor}");
Console.WriteLine($"Hash       : {hash}");
Console.WriteLine();
Console.WriteLine("-- SQL para aplicar:");
Console.WriteLine($"EXEC Vendedores_MActualizarClave @VendedoresID=1, @EmpresaID=1, @ClaveHash='{hash}';");
Console.WriteLine();

// Verificación
bool ok = BCrypt.Net.BCrypt.Verify(password, hash);
Console.WriteLine($"Verificación: {(ok ? "OK ✓" : "FALLÓ ✗")}");
