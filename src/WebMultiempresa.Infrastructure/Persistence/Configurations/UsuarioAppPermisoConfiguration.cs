using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class UsuarioAppPermisoConfiguration : IEntityTypeConfiguration<UsuarioAppPermiso>
{
    public void Configure(EntityTypeBuilder<UsuarioAppPermiso> builder)
    {
        builder.ToTable("UsuarioAppPermisos");
        builder.HasKey(u => u.UsuarioAppPermisosID);
        builder.Property(u => u.UsuarioAppPermisosID).UseIdentityColumn();
        builder.Property(u => u.Permiso).HasMaxLength(100).IsRequired();
        builder.Property(u => u.Baja).HasDefaultValue(false);
        builder.HasOne<Usuario>().WithMany().HasForeignKey(u => u.UsuariosID)
               .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Aplicacion>().WithMany().HasForeignKey(u => u.AplicacionesID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(u => new { u.UsuariosID, u.AplicacionesID, u.Permiso }).IsUnique();
    }
}
