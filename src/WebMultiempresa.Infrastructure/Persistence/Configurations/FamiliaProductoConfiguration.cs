using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class FamiliaProductoConfiguration : IEntityTypeConfiguration<FamiliaProducto>
{
    public void Configure(EntityTypeBuilder<FamiliaProducto> builder)
    {
        builder.ToTable("FamiliaProductos");
        builder.HasKey(m => m.FamiliaProductosID);
        builder.Property(m => m.FamiliaProductosID).UseIdentityColumn();
        builder.Property(m => m.Nombre).HasMaxLength(100).IsRequired();
        builder.Property(m => m.Baja).HasDefaultValue(false);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(m => m.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
