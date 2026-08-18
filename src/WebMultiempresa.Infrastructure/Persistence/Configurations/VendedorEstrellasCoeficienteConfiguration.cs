using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class VendedorEstrellasCoeficienteConfiguration : IEntityTypeConfiguration<VendedorEstrellasCoeficiente>
{
    public void Configure(EntityTypeBuilder<VendedorEstrellasCoeficiente> builder)
    {
        builder.ToTable("VendedorEstrellasCoeficientes");
        builder.HasKey(c => c.VendedorEstrellasCoeficientesID);
        builder.Property(c => c.VendedorEstrellasCoeficientesID).UseIdentityColumn();
        builder.Property(c => c.CantidadEstrellas).IsRequired();
        builder.Property(c => c.CoeficienteComision).HasColumnType("decimal(10,4)").IsRequired();
        builder.Property(c => c.Baja).HasDefaultValue(false);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(c => c.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        // Único coeficiente ACTIVO por cantidad de estrellas por empresa
        builder.HasIndex(c => new { c.EmpresaID, c.CantidadEstrellas })
               .IsUnique()
               .HasFilter("[Baja] = 0");
    }
}
