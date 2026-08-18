using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class SucursalConfiguration : IEntityTypeConfiguration<Sucursal>
{
    public void Configure(EntityTypeBuilder<Sucursal> builder)
    {
        builder.ToTable("Sucursales");
        builder.HasKey(s => s.SucursalesID);
        builder.Property(s => s.SucursalesID).UseIdentityColumn();
        builder.Property(s => s.Nombre).HasMaxLength(200).IsRequired();
        builder.Property(s => s.Baja).HasDefaultValue(false);
        // Restrict evita cascade delete físico desde Empresa hacia Sucursales (soft-delete es el camino correcto)
        builder.HasOne<Empresa>().WithMany().HasForeignKey(s => s.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
