using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class VendedorEstrellaDiariaConfiguration : IEntityTypeConfiguration<VendedorEstrellaDiaria>
{
    public void Configure(EntityTypeBuilder<VendedorEstrellaDiaria> builder)
    {
        builder.ToTable("VendedorEstrellasDiarias");
        builder.HasKey(ed => ed.VendedorEstrellaDiariasID);
        builder.Property(ed => ed.VendedorEstrellaDiariasID).UseIdentityColumn();
        builder.Property(ed => ed.Fecha).HasColumnType("date").IsRequired();
        builder.Property(ed => ed.EstaEncendida).IsRequired();
        builder.Property(ed => ed.Valor).HasColumnType("decimal(18,4)");
        builder.Property(ed => ed.Baja).HasDefaultValue(false);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(ed => ed.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Vendedor>().WithMany().HasForeignKey(ed => ed.VendedoresID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<VendedorEstrellasDefinicion>().WithMany()
               .HasForeignKey(ed => ed.VendedorEstrellasDefinicionesID)
               .OnDelete(DeleteBehavior.Restrict);
        // Una sola entrada por vendedor+empresa+día+definición
        builder.HasIndex(ed => new { ed.VendedoresID, ed.EmpresaID, ed.Fecha, ed.VendedorEstrellasDefinicionesID })
               .IsUnique();
    }
}
