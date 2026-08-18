using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

public sealed class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumento>
{
    public void Configure(EntityTypeBuilder<TipoDocumento> builder)
    {
        builder.ToTable("TiposDocumentos");
        builder.HasKey(t => t.TiposDocumentosID);
        builder.Property(t => t.TiposDocumentosID).ValueGeneratedNever();
        builder.Property(t => t.Nombre).IsRequired().HasMaxLength(50);
    }
}
