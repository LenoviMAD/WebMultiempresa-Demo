using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
        builder.HasKey(c => c.ClientesID);
        builder.Property(c => c.ClientesID).UseIdentityColumn();
        builder.Property(c => c.Codigo).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Nombre).HasMaxLength(300).IsRequired();
        builder.Property(c => c.Direccion).HasMaxLength(300);
        builder.Property(c => c.Localidad).HasMaxLength(200);
        builder.Property(c => c.Provincia).HasMaxLength(100);
        builder.Property(c => c.CodigoPostal).HasMaxLength(20);
        builder.Property(c => c.Telefono).HasMaxLength(100);
        builder.Property(c => c.Email).HasMaxLength(200);
        builder.Property(c => c.Documento).HasMaxLength(50);
        builder.Property(c => c.DiaDeVenta).HasDefaultValue(0);
        builder.HasOne<TipoDocumento>().WithMany().HasForeignKey(c => c.TiposDocumentosID)
               .IsRequired(false).OnDelete(DeleteBehavior.Restrict);
        builder.Property(c => c.ListaDePrecioID).HasDefaultValue(0);
        builder.Property(c => c.ConsumidorFinal).HasDefaultValue(false);
        builder.Property(c => c.PorcentajePercepcionIB).HasColumnType("decimal(5,2)").HasDefaultValue(0m);
        builder.Property(c => c.Latitud).HasColumnType("decimal(18,10)");
        builder.Property(c => c.Longitud).HasColumnType("decimal(18,10)");
        builder.Property(c => c.ClaveHash).HasMaxLength(500);
        builder.Property(c => c.Baja).HasDefaultValue(false);
        builder.Property(c => c.FechaCreacion).HasColumnType("datetime2")
               .HasDefaultValueSql("GETUTCDATE()");
        builder.HasOne<Empresa>().WithMany().HasForeignKey(c => c.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(c => new { c.EmpresaID, c.Codigo }).IsUnique();
        builder.HasIndex(c => new { c.EmpresaID, c.ListaDePrecioID });
    }
}
