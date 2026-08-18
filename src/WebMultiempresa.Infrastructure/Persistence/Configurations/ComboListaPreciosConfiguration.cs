using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ComboListaPreciosConfiguration : IEntityTypeConfiguration<ComboListaPrecios>
{
    public void Configure(EntityTypeBuilder<ComboListaPrecios> builder)
    {
        builder.ToTable("ComboListasPrecios");
        builder.HasKey(l => l.ComboListaPreciosID);
        builder.Property(l => l.ComboListaPreciosID).UseIdentityColumn();
        builder.HasOne<ListaPrecios>().WithMany().HasForeignKey(l => l.ListasPreciosID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
