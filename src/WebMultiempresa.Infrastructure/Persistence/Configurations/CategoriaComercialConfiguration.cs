using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class CategoriaComercialConfiguration : IEntityTypeConfiguration<CategoriaComercial>
{
    public void Configure(EntityTypeBuilder<CategoriaComercial> builder)
    {
        builder.ToTable("CategoriasComerciales");
        builder.HasKey(c => c.CategoriasComercialesID);
        builder.Property(c => c.CategoriasComercialesID).UseIdentityColumn();
        builder.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Baja).HasDefaultValue(false);
        builder.HasOne<Empresa>().WithMany().HasForeignKey(c => c.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(c => c.EmpresaID);
    }
}
