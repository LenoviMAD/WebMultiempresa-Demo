using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ComboLogConfiguration : IEntityTypeConfiguration<ComboLog>
{
    public void Configure(EntityTypeBuilder<ComboLog> builder)
    {
        builder.ToTable("ComboLogs");
        builder.HasKey(l => l.ComboLogsID);
        builder.Property(l => l.ComboLogsID).UseIdentityColumn();
        builder.Property(l => l.NombreUsuario).HasMaxLength(150).IsRequired();
        builder.Property(l => l.PC).HasMaxLength(50).IsRequired();
        builder.Property(l => l.Comentario).HasMaxLength(500).IsRequired();
        builder.Property(l => l.Fecha).HasDefaultValueSql("GETUTCDATE()");
    }
}
