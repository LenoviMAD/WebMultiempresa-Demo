using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ComboSucursalConfiguration : IEntityTypeConfiguration<ComboSucursal>
{
    public void Configure(EntityTypeBuilder<ComboSucursal> builder)
    {
        builder.ToTable("ComboSucursales");
        builder.HasKey(s => s.ComboSucursalID);
        builder.Property(s => s.ComboSucursalID).UseIdentityColumn();
        builder.HasOne<Sucursal>().WithMany().HasForeignKey(s => s.SucursalesID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
