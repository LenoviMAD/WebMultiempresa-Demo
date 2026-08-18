using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ComboVendedorConfiguration : IEntityTypeConfiguration<ComboVendedor>
{
    public void Configure(EntityTypeBuilder<ComboVendedor> builder)
    {
        builder.ToTable("ComboVendedores");
        builder.HasKey(v => v.ComboVendedorID);
        builder.Property(v => v.ComboVendedorID).UseIdentityColumn();
        builder.HasOne<Vendedor>().WithMany().HasForeignKey(v => v.VendedoresID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
