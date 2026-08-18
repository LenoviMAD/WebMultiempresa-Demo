using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ComboFechaConfiguration : IEntityTypeConfiguration<ComboFecha>
{
    public void Configure(EntityTypeBuilder<ComboFecha> builder)
    {
        builder.ToTable("ComboFechas");
        builder.HasKey(f => f.ComboFechasID);
        builder.Property(f => f.ComboFechasID).UseIdentityColumn();
        builder.Property(f => f.FechaInicial).IsRequired();
        builder.Property(f => f.FechaFinal).IsRequired();
    }
}
