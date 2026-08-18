using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class EmpresaAplicacionConfiguration : IEntityTypeConfiguration<EmpresaAplicacion>
{
    public void Configure(EntityTypeBuilder<EmpresaAplicacion> builder)
    {
        builder.ToTable("EmpresaAplicaciones");
        builder.HasKey(e => e.EmpresaAplicacionesID);
        builder.Property(e => e.EmpresaAplicacionesID).UseIdentityColumn();
        builder.Property(e => e.FechaActivacion).HasColumnType("datetime2");
        builder.Property(e => e.FechaVencimiento).HasColumnType("datetime2");
        builder.Property(e => e.Baja).HasDefaultValue(false);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(e => e.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Aplicacion>().WithMany().HasForeignKey(e => e.AplicacionesID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(e => new { e.EmpresaID, e.AplicacionesID }).IsUnique();
    }
}
