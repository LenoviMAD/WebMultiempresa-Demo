using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class CategoriaProductoConfiguration : IEntityTypeConfiguration<CategoriaProducto>
{
    public void Configure(EntityTypeBuilder<CategoriaProducto> builder)
    {
        builder.ToTable("CategoriaProductos");
        builder.HasKey(c => c.CategoriasProductosID);
        builder.Property(c => c.CategoriasProductosID).UseIdentityColumn();
        builder.Property(c => c.Nombre).HasMaxLength(150).IsRequired();
        builder.Property(c => c.UrlImagen).HasMaxLength(350).HasDefaultValue("");
        builder.Property(c => c.Baja).HasDefaultValue(false);

        builder.HasOne<Empresa>().WithMany().HasForeignKey(c => c.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Relaciones)
               .WithOne(r => r.Categoria)
               .HasForeignKey(r => r.CategoriasProductosID)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
