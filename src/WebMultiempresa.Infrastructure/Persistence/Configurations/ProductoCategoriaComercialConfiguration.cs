using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ProductoCategoriaComercialConfiguration : IEntityTypeConfiguration<ProductoCategoriaComercial>
{
    public void Configure(EntityTypeBuilder<ProductoCategoriaComercial> builder)
    {
        builder.ToTable("ProductoCategoriasComerciales");
        builder.HasKey(pc => pc.ProductoCategoriasComercialID);
        builder.Property(pc => pc.ProductoCategoriasComercialID).UseIdentityColumn();
        builder.HasOne<Producto>().WithMany().HasForeignKey(pc => pc.ProductosID)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<CategoriaComercial>().WithMany().HasForeignKey(pc => pc.CategoriasComercialesID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(pc => pc.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(pc => new { pc.ProductosID, pc.CategoriasComercialesID }).IsUnique();
        builder.HasIndex(pc => pc.EmpresaID);
    }
}
