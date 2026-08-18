using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ClienteVendedorConfiguration : IEntityTypeConfiguration<ClienteVendedor>
{
    public void Configure(EntityTypeBuilder<ClienteVendedor> builder)
    {
        builder.ToTable("ClienteVendedores");
        builder.HasKey(c => c.ClienteVendedoresID);
        builder.Property(c => c.ClienteVendedoresID).UseIdentityColumn();
        builder.Property(c => c.Baja).HasDefaultValue(false);
        // FKs hacia tablas legacy — EF no genera constraints en BD
        builder.HasOne<Cliente>().WithMany().HasForeignKey(c => c.ClientesID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Vendedor>().WithMany().HasForeignKey(c => c.VendedoresID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(c => new { c.ClientesID, c.VendedoresID }).IsUnique();
    }
}
