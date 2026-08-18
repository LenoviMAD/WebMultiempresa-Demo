using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class GpsPosicionConfiguration : IEntityTypeConfiguration<GpsPosicion>
{
    public void Configure(EntityTypeBuilder<GpsPosicion> builder)
    {
        // Tabla de alto volumen: BIGINT PK, sin Baja, sin Global Query Filter
        builder.ToTable("GpsPosiciones");
        builder.HasKey(g => g.GpsPosicionesID);
        builder.Property(g => g.GpsPosicionesID).UseIdentityColumn();
        builder.Property(g => g.Latitud).HasColumnType("decimal(10,7)");
        builder.Property(g => g.Longitud).HasColumnType("decimal(11,7)");
        builder.Property(g => g.VelocidadKmh).HasColumnType("decimal(5,1)");
        builder.Property(g => g.DistanciaMetros).HasColumnType("decimal(10,2)");
        builder.Property(g => g.ModeloDispositivo).HasMaxLength(100);
        builder.Property(g => g.NumeroDispositivo).HasMaxLength(50);
        builder.Property(g => g.OrigenEnvio).HasMaxLength(50);
        builder.Property(g => g.FechaHora).HasColumnType("datetime2");
        builder.HasOne<Aplicacion>().WithMany().HasForeignKey(g => g.AplicacionesID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Vendedor>().WithMany().HasForeignKey(g => g.VendedoresID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(g => new { g.EmpresaID, g.VendedoresID, g.FechaHora });
    }
}
