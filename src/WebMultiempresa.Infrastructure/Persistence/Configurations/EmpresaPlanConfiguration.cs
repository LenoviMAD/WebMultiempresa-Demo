using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class EmpresaPlanConfiguration : IEntityTypeConfiguration<EmpresaPlan>
{
    public void Configure(EntityTypeBuilder<EmpresaPlan> builder)
    {
        builder.ToTable("EmpresaPlanes");
        builder.HasKey(e => e.EmpresaPlanesID);
        builder.Property(e => e.EmpresaPlanesID).UseIdentityColumn();
        builder.Property(e => e.FechaInicio).HasColumnType("datetime2");
        builder.Property(e => e.FechaVencimiento).HasColumnType("datetime2");
        builder.Property(e => e.Baja).HasDefaultValue(false);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(e => e.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Plan>().WithMany().HasForeignKey(e => e.PlanesID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
