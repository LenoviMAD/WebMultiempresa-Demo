using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class PedidoDetalleConfiguration : IEntityTypeConfiguration<PedidoDetalle>
{
    public void Configure(EntityTypeBuilder<PedidoDetalle> builder)
    {
        builder.ToTable("PedidosDetalle");
        builder.HasKey(d => d.PedidosDetalleID);
        builder.Property(d => d.PedidosDetalleID).UseIdentityColumn();
        builder.Property(d => d.Cantidad).HasColumnType("decimal(18,3)").IsRequired();
        builder.Property(d => d.Precio).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(d => d.Descuento1).HasColumnType("decimal(5,2)").HasDefaultValue(0m);
        builder.Property(d => d.Descuento2).HasColumnType("decimal(5,2)").HasDefaultValue(0m);
        builder.Property(d => d.EsCombo).HasDefaultValue(false);
        builder.Property(d => d.ListasPreciosID).IsRequired();
        builder.Property(d => d.UnidadesPorBulto).HasColumnType("decimal(18,3)");
        builder.HasOne<Pedido>().WithMany().HasForeignKey(d => d.PedidosID)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(d => d.PedidosID);
    }
}
