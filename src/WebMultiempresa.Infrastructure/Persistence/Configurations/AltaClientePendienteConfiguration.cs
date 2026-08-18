using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class AltaClientePendienteConfiguration : IEntityTypeConfiguration<AltaClientePendiente>
{
    public void Configure(EntityTypeBuilder<AltaClientePendiente> builder)
    {
        builder.ToTable("AltaClientesPendientes");
        builder.HasKey(a => a.AltaClientesPendientesID);
        builder.Property(a => a.AltaClientesPendientesID).UseIdentityColumn();
        builder.Property(a => a.TipoUsuario).HasDefaultValue((byte)1);
        builder.Property(a => a.Nombre).HasMaxLength(200).IsRequired();
        builder.Property(a => a.Documento).HasMaxLength(20);
        builder.Property(a => a.Email).HasMaxLength(200);
        builder.Property(a => a.Telefono).HasMaxLength(50);
        builder.Property(a => a.TelefonoNormalizado).HasMaxLength(20);
        builder.Property(a => a.Localidad).HasMaxLength(100);
        builder.Property(a => a.Provincia).HasMaxLength(100);
        builder.Property(a => a.DireccionEnvio).HasMaxLength(300);
        builder.Property(a => a.CodigoPostal).HasMaxLength(20);
        builder.Property(a => a.Latitud).HasColumnType("decimal(18,10)");
        builder.Property(a => a.Longitud).HasColumnType("decimal(18,10)");
        builder.Property(a => a.Password).HasMaxLength(500);
        builder.Property(a => a.Estado).HasDefaultValue((byte)0);
        builder.Property(a => a.ErrorDetalle).HasMaxLength(1000);
        builder.Property(a => a.FechaSolicitud).HasColumnType("datetime2")
               .HasDefaultValueSql("GETUTCDATE()");
        builder.Property(a => a.FechaConfirmacion).HasColumnType("datetime2");
        builder.Property(a => a.FechaProcesamiento).HasColumnType("datetime2");
        builder.HasOne<Empresa>().WithMany().HasForeignKey(a => a.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(a => new { a.EmpresaID, a.Estado });
        builder.HasIndex(a => a.TelefonoNormalizado);
    }
}
