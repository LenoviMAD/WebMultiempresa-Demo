using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class VendedorConfiguration : IEntityTypeConfiguration<Vendedor>
{
    public void Configure(EntityTypeBuilder<Vendedor> builder)
    {
        builder.ToTable("Vendedores");
        builder.HasKey(v => v.VendedoresID);
        builder.Property(v => v.VendedoresID).UseIdentityColumn();
        builder.Property(v => v.Codigo).HasMaxLength(50).IsRequired();
        builder.Property(v => v.Nombre).HasMaxLength(200).IsRequired();
        builder.Property(v => v.WhatsApp).HasMaxLength(50);
        builder.Property(v => v.Clave).HasMaxLength(200);
        builder.Property(v => v.DiaInicioRuta).HasDefaultValue(0);
        builder.Property(v => v.VerTodasLasCategorias).HasDefaultValue(false);
        builder.Property(v => v.Baja).HasDefaultValue(false);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(v => v.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<CategoriaComercial>().WithMany().HasForeignKey(v => v.CategoriasComercialesID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(v => new { v.EmpresaID, v.Codigo }).IsUnique();
    }
}
