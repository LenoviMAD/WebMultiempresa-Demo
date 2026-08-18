using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ImpuestoConfiguration : IEntityTypeConfiguration<Impuesto>
{
    public void Configure(EntityTypeBuilder<Impuesto> builder)
    {
        builder.ToTable("Impuestos");
        builder.HasKey(i => i.ImpuestosID);
        builder.Property(i => i.ImpuestosID).UseIdentityColumn();
        builder.Property(i => i.Nombre).HasMaxLength(50).IsRequired();
        builder.Property(i => i.Porcentaje).HasPrecision(5, 2);
        builder.Property(i => i.Baja).HasDefaultValue(false);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(i => i.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(i => i.EmpresaID);
    }
}
