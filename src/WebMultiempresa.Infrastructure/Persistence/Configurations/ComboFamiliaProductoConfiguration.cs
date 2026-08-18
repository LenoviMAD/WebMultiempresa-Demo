using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ComboFamiliaProductoConfiguration : IEntityTypeConfiguration<ComboFamiliaProducto>
{
    public void Configure(EntityTypeBuilder<ComboFamiliaProducto> builder)
    {
        builder.ToTable("ComboFamiliaProductos");
        builder.HasKey(m => m.ComboFamiliaProductosID);
        builder.Property(m => m.ComboFamiliaProductosID).UseIdentityColumn();
        builder.HasIndex(m => new { m.CombosID, m.FamiliaProductosID }).IsUnique();
        builder.HasOne(m => m.FamiliaProducto).WithMany().HasForeignKey(m => m.FamiliaProductosID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
