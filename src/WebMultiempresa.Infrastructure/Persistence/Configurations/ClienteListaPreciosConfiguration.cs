using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ClienteListaPreciosConfiguration : IEntityTypeConfiguration<ClienteListaPrecios>
{
    public void Configure(EntityTypeBuilder<ClienteListaPrecios> builder)
    {
        builder.ToTable("ClienteListasPrecios");
        builder.HasKey(c => c.ClienteListasPreciosID);
        builder.Property(c => c.ClienteListasPreciosID).UseIdentityColumn();
        builder.Property(c => c.EsPrincipal).HasDefaultValue(false);
        builder.Property(c => c.Baja).HasDefaultValue(false);
        // FK hacia tabla legacy — EF no genera constraint en BD
        builder.HasOne<Cliente>().WithMany().HasForeignKey(c => c.ClientesID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<ListaPrecios>().WithMany().HasForeignKey(c => c.ListasPreciosID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(c => new { c.ClientesID, c.ListasPreciosID }).IsUnique();
    }
}
