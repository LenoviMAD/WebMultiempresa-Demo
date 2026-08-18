using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Enums;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ActorEstadoLogConfiguration : IEntityTypeConfiguration<ActorEstadoLog>
{
    public void Configure(EntityTypeBuilder<ActorEstadoLog> builder)
    {
        builder.ToTable("ActorEstadoLog");
        builder.HasKey(l => l.ActorEstadoLogID);
        builder.Property(l => l.ActorEstadoLogID).UseIdentityColumn();
        builder.Property(l => l.TipoEvento)
               .HasMaxLength(10)
               .HasConversion(
                   v => v.ToString(),
                   v => Enum.Parse<TipoEventoActor>(v));
        builder.Property(l => l.FechaEvento).HasColumnType("datetime2");
        builder.HasOne<TipoActor>().WithMany()
               .HasForeignKey(l => l.TiposActoresID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(l => new { l.TiposActoresID, l.EmpresaID, l.TipoEvento, l.FechaEvento })
               .IncludeProperties(l => l.ActorID);
    }
}
