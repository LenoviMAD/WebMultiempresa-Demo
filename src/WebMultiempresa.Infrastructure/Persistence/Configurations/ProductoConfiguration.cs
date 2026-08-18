using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("Productos");
        builder.HasKey(p => p.ProductosID);
        builder.Property(p => p.ProductosID).UseIdentityColumn();
        builder.Property(p => p.Codigo).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Nombre).HasMaxLength(200).IsRequired();
        builder.Property(p => p.UrlImagenWeb).HasMaxLength(500);
        builder.Property(p => p.PrecioCosto).HasColumnType("decimal(18,4)");
        builder.HasOne<Impuesto>().WithMany().HasForeignKey(p => p.ImpuestosID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.Property(p => p.CantidadMultiplo).HasDefaultValue(1);
        builder.Property(p => p.UnidadesPorBulto).HasDefaultValue(1);
        builder.Property(p => p.StockEnUnidades).HasDefaultValue(0);
        builder.Property(p => p.StockPropio).HasDefaultValue(true);
        builder.Property(p => p.DesactivadoParaLaVenta).HasDefaultValue(false);
        builder.Property(p => p.Baja).HasDefaultValue(false);
        builder.Property(p => p.FechaAlta).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(p => p.FechaActualizacion).HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne<Empresa>().WithMany().HasForeignKey(p => p.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Producto>().WithMany().HasForeignKey(p => p.ProductosIDPadre)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<MarcaProducto>().WithMany().HasForeignKey(p => p.MarcasProductosID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<FamiliaProducto>().WithMany().HasForeignKey(p => p.FamiliaProductosID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
