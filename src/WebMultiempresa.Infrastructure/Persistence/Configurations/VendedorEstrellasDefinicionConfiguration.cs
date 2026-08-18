using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class VendedorEstrellasDefinicionConfiguration : IEntityTypeConfiguration<VendedorEstrellasDefinicion>
{
    public void Configure(EntityTypeBuilder<VendedorEstrellasDefinicion> builder)
    {
        builder.ToTable("VendedorEstrellasDefiniciones");
        builder.HasKey(e => e.VendedorEstrellasDefinicionesID);
        builder.Property(e => e.VendedorEstrellasDefinicionesID).UseIdentityColumn();
        builder.Property(e => e.NumeroEstrella).IsRequired();
        builder.Property(e => e.Nombre).HasMaxLength(200).IsRequired();
        builder.Property(e => e.ObjetivoMensual).HasColumnType("decimal(18,4)");
        builder.Property(e => e.Baja).HasDefaultValue(false);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(e => e.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        // Única definición ACTIVA por número de estrella por empresa
        // Filtrado: permite que filas con Baja=1 conserven el mismo número sin conflicto
        builder.HasIndex(e => new { e.EmpresaID, e.NumeroEstrella })
               .IsUnique()
               .HasFilter("[Baja] = 0");
    }
}
