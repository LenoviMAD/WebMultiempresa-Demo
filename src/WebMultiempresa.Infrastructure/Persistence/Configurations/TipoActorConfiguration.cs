using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class TipoActorConfiguration : IEntityTypeConfiguration<TipoActor>
{
    public void Configure(EntityTypeBuilder<TipoActor> builder)
    {
        builder.ToTable("TiposActores");
        builder.HasKey(t => t.TiposActoresID);
        builder.Property(t => t.TiposActoresID).UseIdentityColumn();
        builder.Property(t => t.Nombre).HasMaxLength(200).IsRequired();
        builder.Property(t => t.Codigo).HasMaxLength(50).IsRequired();
        builder.Property(t => t.Baja).HasDefaultValue(false);
        builder.HasOne<Aplicacion>().WithMany().HasForeignKey(t => t.AplicacionesID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(t => new { t.AplicacionesID, t.Codigo }).IsUnique();
    }
}
