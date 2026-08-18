using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ProductoSubCategoriaConfiguration : IEntityTypeConfiguration<ProductoSubCategoria>
{
    public void Configure(EntityTypeBuilder<ProductoSubCategoria> builder)
    {
        builder.ToTable("ProductoSubCategorias");
        builder.HasKey(r => r.ProductoSubCategoriasID);
        builder.Property(r => r.ProductoSubCategoriasID).UseIdentityColumn();
        builder.Property(r => r.Baja).HasDefaultValue(false);

        builder.HasIndex(r => new { r.ProductosID, r.SubCategoriasProductosID }).IsUnique();

        builder.HasOne<Producto>().WithMany().HasForeignKey(r => r.ProductosID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<SubCategoriaProducto>().WithMany().HasForeignKey(r => r.SubCategoriasProductosID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(r => r.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
