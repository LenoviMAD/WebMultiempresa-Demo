using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class VendedorEstadisticaConfiguration : IEntityTypeConfiguration<VendedorEstadistica>
{
    public void Configure(EntityTypeBuilder<VendedorEstadistica> builder)
    {
        builder.ToTable("VendedorEstadisticas");
        builder.HasKey(e => e.VendedorEstadisticasID);
        builder.Property(e => e.VendedorEstadisticasID).UseIdentityColumn();
        builder.Property(e => e.Fecha).HasColumnType("date").IsRequired();
        builder.Property(e => e.CoeficienteComision).HasColumnType("decimal(10,4)");
        builder.Property(e => e.Baja).HasDefaultValue(false);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(e => e.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Vendedor>().WithMany().HasForeignKey(e => e.VendedoresID)
               .OnDelete(DeleteBehavior.Restrict);
        // Un registro por vendedor+empresa por día
        builder.HasIndex(e => new { e.VendedoresID, e.EmpresaID, e.Fecha }).IsUnique();
    }
}
