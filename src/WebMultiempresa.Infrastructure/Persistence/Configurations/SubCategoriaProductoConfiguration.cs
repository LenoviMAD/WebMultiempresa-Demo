using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class SubCategoriaProductoConfiguration : IEntityTypeConfiguration<SubCategoriaProducto>
{
    public void Configure(EntityTypeBuilder<SubCategoriaProducto> builder)
    {
        builder.ToTable("SubCategoriaProductos");
        builder.HasKey(s => s.SubCategoriasProductosID);
        builder.Property(s => s.SubCategoriasProductosID).UseIdentityColumn();
        builder.Property(s => s.Nombre).HasMaxLength(150).IsRequired();
        builder.Property(s => s.UrlImagen).HasMaxLength(350).HasDefaultValue("");
        builder.Property(s => s.Baja).HasDefaultValue(false);

        builder.HasOne<Empresa>().WithMany().HasForeignKey(s => s.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Relaciones)
               .WithOne(r => r.SubCategoria)
               .HasForeignKey(r => r.SubCategoriasProductosID)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
