using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ComboItemConfiguration : IEntityTypeConfiguration<ComboItem>
{
    public void Configure(EntityTypeBuilder<ComboItem> builder)
    {
        builder.ToTable("ComboItems");
        builder.HasKey(i => i.ComboItemsID);
        builder.Property(i => i.ComboItemsID).UseIdentityColumn();
        builder.Property(i => i.Cantidad).HasPrecision(18, 4).IsRequired();
        builder.Property(i => i.Precio).HasPrecision(18, 4).IsRequired();
        builder.Property(i => i.Descuento1).HasPrecision(10, 2).IsRequired();
        builder.Property(i => i.Descuento2).HasPrecision(10, 2).IsRequired();
        // Restrict evita cascade delete físico desde Producto hacia ComboItems
        builder.HasOne(i => i.Producto).WithMany().HasForeignKey(i => i.ProductosID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
