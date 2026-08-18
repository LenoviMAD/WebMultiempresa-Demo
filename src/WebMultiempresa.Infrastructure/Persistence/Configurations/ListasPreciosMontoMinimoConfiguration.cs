using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ListasPreciosMontoMinimoConfiguration : IEntityTypeConfiguration<ListasPreciosMontoMinimo>
{
    public void Configure(EntityTypeBuilder<ListasPreciosMontoMinimo> builder)
    {
        builder.ToTable("ListasPreciosMontoMinimo");
        builder.HasKey(m => m.ListasPreciosMontoMinimoID);
        builder.Property(m => m.ListasPreciosMontoMinimoID).UseIdentityColumn();
        builder.Property(m => m.MontoMinimo).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(m => m.Baja).HasDefaultValue(false);

        builder.HasOne<ListaPrecios>()
               .WithMany()
               .HasForeignKey(m => m.ListasPreciosID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Empresa>()
               .WithMany()
               .HasForeignKey(m => m.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);

        // Un registro de monto mínimo por lista por empresa
        builder.HasIndex(m => new { m.ListasPreciosID, m.EmpresaID }).IsUnique();
    }
}
