using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.ToTable("Empresas");
        builder.HasKey(e => e.EmpresaID);
        builder.Property(e => e.EmpresaID).UseIdentityColumn();
        builder.Property(e => e.Nombre).HasMaxLength(500).IsRequired();
        builder.Property(e => e.KeyConexion).HasMaxLength(50).IsRequired();
        builder.HasIndex(e => e.KeyConexion).IsUnique();
        builder.Property(e => e.Baja).HasDefaultValue(false);
    }
}
