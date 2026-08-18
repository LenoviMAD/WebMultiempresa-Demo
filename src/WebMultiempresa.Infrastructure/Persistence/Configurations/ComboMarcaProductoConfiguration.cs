using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ComboMarcaProductoConfiguration : IEntityTypeConfiguration<ComboMarcaProducto>
{
    public void Configure(EntityTypeBuilder<ComboMarcaProducto> builder)
    {
        builder.ToTable("ComboMarcasProductos");
        builder.HasKey(r => r.ComboMarcasProductosID);
        builder.Property(r => r.ComboMarcasProductosID).UseIdentityColumn();
        builder.HasIndex(r => new { r.CombosID, r.MarcasProductosID }).IsUnique();
        builder.HasOne(r => r.MarcaProducto).WithMany().HasForeignKey(r => r.MarcasProductosID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
