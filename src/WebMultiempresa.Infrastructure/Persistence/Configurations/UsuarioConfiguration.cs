using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");
        builder.HasKey(u => u.UsuariosID);
        builder.Property(u => u.UsuariosID).UseIdentityColumn();
        builder.Property(u => u.Email).HasMaxLength(200).IsRequired();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.PasswordHash).HasMaxLength(256).IsRequired();
        builder.Property(u => u.Nombre).HasMaxLength(200).IsRequired();
        builder.Property(u => u.Rol).IsRequired();
        builder.Property(u => u.Baja).HasDefaultValue(false);
        builder.Property(u => u.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
        builder.HasOne(u => u.Empresa)
               .WithMany()
               .HasForeignKey(u => u.EmpresaID)
               .IsRequired(false);
    }
}
