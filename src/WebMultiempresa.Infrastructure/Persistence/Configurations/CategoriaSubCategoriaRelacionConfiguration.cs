using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class CategoriaSubCategoriaRelacionConfiguration : IEntityTypeConfiguration<CategoriaSubCategoriaRelacion>
{
    public void Configure(EntityTypeBuilder<CategoriaSubCategoriaRelacion> builder)
    {
        builder.ToTable("CategoriaSubCategoriasRelaciones");
        builder.HasKey(r => r.CategoriaSubCategoriaRelacionID);
        builder.Property(r => r.CategoriaSubCategoriaRelacionID).UseIdentityColumn();

        // FK sin cascade doble: la cascada viene desde CategoriaProducto y SubCategoriaProducto
        builder.HasOne(r => r.Categoria)
               .WithMany(c => c.Relaciones)
               .HasForeignKey(r => r.CategoriasProductosID)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.SubCategoria)
               .WithMany(s => s.Relaciones)
               .HasForeignKey(r => r.SubCategoriasProductosID)
               .OnDelete(DeleteBehavior.NoAction);

        // Unicidad: una subcategoría no puede estar dos veces en la misma categoría de la misma empresa
        builder.HasIndex(r => new { r.EmpresaID, r.CategoriasProductosID, r.SubCategoriasProductosID })
               .IsUnique();
    }
}
