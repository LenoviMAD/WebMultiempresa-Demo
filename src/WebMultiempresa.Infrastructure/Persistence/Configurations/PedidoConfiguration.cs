using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");
        builder.HasKey(p => p.PedidosID);
        builder.Property(p => p.PedidosID).UseIdentityColumn();
        builder.Property(p => p.DeviceID).HasMaxLength(100);
        builder.Property(p => p.ClienteCodigo).HasMaxLength(50);
        builder.Property(p => p.ClienteDireccion).HasMaxLength(300);
        builder.Property(p => p.Division).HasMaxLength(50);
        builder.Property(p => p.Comentario).HasMaxLength(500);
        builder.Property(p => p.FechaCarga).HasColumnType("datetime2");
        builder.Property(p => p.FechaTx).HasColumnType("datetime2");
        builder.Property(p => p.TotalPedido).HasColumnType("decimal(18,2)").HasDefaultValue(0m);
        builder.Property(p => p.EstadoArrastre).HasColumnType("decimal(18,2)").HasDefaultValue(0m);
        builder.Property(p => p.ConsumidorFinal).HasDefaultValue(false);
        builder.Property(p => p.TipoCliente).HasDefaultValue((byte)0);
        builder.Property(p => p.Latitud).HasColumnType("decimal(18,10)");
        builder.Property(p => p.Longitud).HasColumnType("decimal(18,10)");
        builder.Property(p => p.ResultadoTx).HasMaxLength(100);
        builder.Property(p => p.FechaEntrega).HasColumnType("datetime2");
        builder.Property(p => p.KeyPagoID).HasMaxLength(100);
        builder.Property(p => p.PaymentID).HasMaxLength(100);
        builder.Property(p => p.PedidoCerrado).HasDefaultValue(false);
        builder.Property(p => p.Baja).HasDefaultValue(false);
        builder.Property(p => p.FechaCreacion).HasColumnType("datetime2")
               .HasDefaultValueSql("GETUTCDATE()");
        builder.HasOne<Empresa>().WithMany().HasForeignKey(p => p.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(p => new { p.EmpresaID, p.VendedoresID, p.FechaCarga });
        builder.HasIndex(p => new { p.EmpresaID, p.ClientesID });
        builder.HasIndex(p => new { p.EmpresaID, p.PedidoCerrado });
    }
}
