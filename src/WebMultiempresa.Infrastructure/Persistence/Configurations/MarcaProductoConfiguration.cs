using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class MarcaProductoConfiguration : IEntityTypeConfiguration<MarcaProducto>
{
    public void Configure(EntityTypeBuilder<MarcaProducto> builder)
    {
        builder.ToTable("MarcasProductos");
        builder.HasKey(r => r.MarcasProductosID);
        builder.Property(r => r.MarcasProductosID).UseIdentityColumn();
        builder.Property(r => r.Nombre).HasMaxLength(150).IsRequired();
        builder.Property(r => r.Baja).HasDefaultValue(false);
        // Restrict evita cascade delete físico desde Empresa (soft-delete es el camino correcto)
        builder.HasOne<Empresa>().WithMany().HasForeignKey(r => r.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
