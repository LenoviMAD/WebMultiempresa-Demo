using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class AplicacionConfiguration : IEntityTypeConfiguration<Aplicacion>
{
    public void Configure(EntityTypeBuilder<Aplicacion> builder)
    {
        builder.ToTable("Aplicaciones");
        builder.HasKey(a => a.AplicacionesID);
        builder.Property(a => a.AplicacionesID).UseIdentityColumn();
        builder.Property(a => a.Nombre).HasMaxLength(200).IsRequired();
        builder.Property(a => a.Descripcion).HasMaxLength(500);
        builder.Property(a => a.Baja).HasDefaultValue(false);
    }
}
