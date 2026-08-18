using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ClienteDocumentoConfiguration : IEntityTypeConfiguration<ClienteDocumento>
{
    public void Configure(EntityTypeBuilder<ClienteDocumento> builder)
    {
        builder.ToTable("ClienteDocumentos");
        builder.HasKey(c => c.ClienteDocumentosID);
        builder.Property(c => c.ClienteDocumentosID).UseIdentityColumn();
        builder.Property(c => c.Numero).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Validado).HasDefaultValue(false);
        builder.Property(c => c.Baja).HasDefaultValue(false);
        builder.HasOne<Cliente>().WithMany()
               .HasForeignKey(c => c.ClientesID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<TipoDocumento>().WithMany()
               .HasForeignKey(c => c.TiposDocumentosID)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(c => new { c.ClientesID, c.EmpresaID, c.TiposDocumentosID }).IsUnique();
    }
}
