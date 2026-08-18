using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ProductoPrecioConfiguration : IEntityTypeConfiguration<ProductoPrecio>
{
    public void Configure(EntityTypeBuilder<ProductoPrecio> builder)
    {
        builder.ToTable("ProductoPrecios");
        builder.HasKey(p => p.ProductoPreciosID);
        builder.Property(p => p.ProductoPreciosID).UseIdentityColumn();
        builder.Property(p => p.PrecioFinal).HasColumnType("decimal(18,4)");
        builder.Property(p => p.Baja).HasDefaultValue(false);
        builder.Property(p => p.FechaActualizacion).HasDefaultValueSql("GETUTCDATE()");
        // FK hacia tabla legacy — EF no genera constraint en BD
        builder.HasOne<Producto>().WithMany().HasForeignKey(p => p.ProductosID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<ListaPrecios>().WithMany().HasForeignKey(p => p.ListasPreciosID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(p => new { p.ProductosID, p.ListasPreciosID }).IsUnique();
    }
}
