using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ListaPreciosConfiguration : IEntityTypeConfiguration<ListaPrecios>
{
    public void Configure(EntityTypeBuilder<ListaPrecios> builder)
    {
        builder.ToTable("ListasPrecios");
        builder.HasKey(l => l.ListasPreciosID);
        builder.Property(l => l.ListasPreciosID).UseIdentityColumn();
        builder.Property(l => l.Nombre).HasMaxLength(100).IsRequired();
        builder.Property(l => l.PorcentajeMarcup).HasPrecision(5, 2).HasDefaultValue(0m);
        builder.Property(l => l.Baja).HasDefaultValue(false);
        // Restrict evita cascade delete físico desde Empresa hacia ListasPrecios (soft-delete es el camino correcto)
        builder.HasOne<Empresa>().WithMany().HasForeignKey(l => l.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
