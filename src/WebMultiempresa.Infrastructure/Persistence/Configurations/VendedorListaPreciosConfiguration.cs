using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class VendedorListaPreciosConfiguration : IEntityTypeConfiguration<VendedorListaPrecios>
{
    public void Configure(EntityTypeBuilder<VendedorListaPrecios> builder)
    {
        builder.ToTable("VendedorListasPrecios");
        builder.HasKey(v => v.VendedorListasPreciosID);
        builder.Property(v => v.VendedorListasPreciosID).UseIdentityColumn();
        builder.Property(v => v.EsDefault).HasDefaultValue(false);
        builder.Property(v => v.Baja).HasDefaultValue(false);
        builder.HasOne<Vendedor>().WithMany()
               .HasForeignKey(v => v.VendedoresID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<ListaPrecios>().WithMany()
               .HasForeignKey(v => v.ListasPreciosID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
