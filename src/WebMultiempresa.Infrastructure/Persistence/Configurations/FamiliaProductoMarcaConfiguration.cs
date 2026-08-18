using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class FamiliaProductoMarcaConfiguration : IEntityTypeConfiguration<FamiliaProductoMarca>
{
    public void Configure(EntityTypeBuilder<FamiliaProductoMarca> builder)
    {
        builder.ToTable("FamiliaProductoMarcas");
        builder.HasKey(r => r.FamiliaProductoMarcasID);
        builder.Property(r => r.FamiliaProductoMarcasID).UseIdentityColumn();
        builder.Property(r => r.Baja).HasDefaultValue(false);

        builder.HasIndex(r => new { r.FamiliaProductosID, r.MarcasProductosID }).IsUnique();

        builder.HasOne<FamiliaProducto>().WithMany().HasForeignKey(r => r.FamiliaProductosID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<MarcaProducto>().WithMany().HasForeignKey(r => r.MarcasProductosID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(r => r.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
