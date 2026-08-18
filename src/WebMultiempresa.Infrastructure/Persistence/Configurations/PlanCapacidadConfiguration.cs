using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class PlanCapacidadConfiguration : IEntityTypeConfiguration<PlanCapacidad>
{
    public void Configure(EntityTypeBuilder<PlanCapacidad> builder)
    {
        builder.ToTable("PlanCapacidades");
        builder.HasKey(p => p.PlanCapacidadesID);
        builder.Property(p => p.PlanCapacidadesID).UseIdentityColumn();
        builder.Property(p => p.MaxCapacidad).IsRequired();
        builder.HasOne<Plan>().WithMany().HasForeignKey(p => p.PlanesID)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<TipoActor>().WithMany().HasForeignKey(p => p.TiposActoresID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(p => new { p.PlanesID, p.TiposActoresID }).IsUnique();
    }
}
