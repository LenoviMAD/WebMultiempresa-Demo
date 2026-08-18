using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable("Planes");
        builder.HasKey(p => p.PlanesID);
        builder.Property(p => p.PlanesID).UseIdentityColumn();
        builder.Property(p => p.Nombre).HasMaxLength(200).IsRequired();
        builder.Property(p => p.Descripcion).HasMaxLength(500);
        builder.Property(p => p.Baja).HasDefaultValue(false);
        builder.HasOne<Aplicacion>().WithMany().HasForeignKey(p => p.AplicacionesID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
