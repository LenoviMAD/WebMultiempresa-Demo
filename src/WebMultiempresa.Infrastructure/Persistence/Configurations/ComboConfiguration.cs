using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Infrastructure.Persistence.Configurations;

internal sealed class ComboConfiguration : IEntityTypeConfiguration<Combo>
{
    public void Configure(EntityTypeBuilder<Combo> builder)
    {
        builder.ToTable("Combos");
        builder.HasKey(c => c.CombosID);
        builder.Property(c => c.CombosID).UseIdentityColumn();
        builder.Property(c => c.Nombre).HasMaxLength(150).IsRequired();
        builder.Property(c => c.NombreAlternativo).HasMaxLength(150);
        builder.Property(c => c.Codigo).HasMaxLength(10).IsRequired();
        builder.HasIndex(c => new { c.EmpresaID, c.Codigo }).IsUnique();
        builder.Property(c => c.CantidadPorFactura).HasPrecision(18, 2);
        builder.Property(c => c.CantidadDinamica).HasPrecision(10, 2);
        builder.Property(c => c.CantidadSinCargo).HasPrecision(7, 2);
        builder.Property(c => c.CantidadXPDV).HasPrecision(10, 2);
        builder.Property(c => c.MontoArrastre).HasPrecision(18, 2);
        builder.Property(c => c.ImporteProductosFuera).HasPrecision(18, 2);
        builder.Property(c => c.PorcentajeComision).HasPrecision(10, 2);
        builder.Property(c => c.TodosLosVendedores).HasDefaultValue(true);
        builder.Property(c => c.TodasLasSucursales).HasDefaultValue(true);
        builder.Property(c => c.EsEstrategico).HasDefaultValue(true);
        builder.Property(c => c.ValidarPartido).HasDefaultValue(true);
        builder.Property(c => c.CantidadSinCargo).HasDefaultValue(1m);
        builder.Property(c => c.Baja).HasDefaultValue(false);
        builder.Property(c => c.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");
        // Restrict evita cascade delete físico desde Empresa hacia Combos (soft-delete es el camino correcto)
        builder.HasOne<Empresa>().WithMany().HasForeignKey(c => c.EmpresaID)
               .OnDelete(DeleteBehavior.Restrict);
        // Restrict evita cascade delete físico desde MarcaProducto hacia Combos — nullable: combo sin marca es válido
        builder.HasOne(c => c.MarcaProducto).WithMany().HasForeignKey(c => c.MarcasProductosID)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(c => c.Items).WithOne().HasForeignKey(i => i.CombosID);
        builder.HasMany(c => c.Fechas).WithOne().HasForeignKey(f => f.CombosID);
        builder.HasMany(c => c.Vendedores).WithOne().HasForeignKey(v => v.CombosID);
        builder.HasMany(c => c.ListasPrecios).WithOne().HasForeignKey(l => l.CombosID);
        builder.HasMany(c => c.Sucursales).WithOne().HasForeignKey(s => s.CombosID);
        builder.HasMany(c => c.MarcasProductosArrastre).WithOne().HasForeignKey(r => r.CombosID);
        builder.HasMany(c => c.FamiliaProductosArrastre).WithOne().HasForeignKey(m => m.CombosID);
        builder.HasMany(c => c.Logs).WithOne().HasForeignKey(l => l.CombosID);

        builder.Navigation(c => c.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(c => c.Fechas).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(c => c.Vendedores).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(c => c.ListasPrecios).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(c => c.Sucursales).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(c => c.MarcasProductosArrastre).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(c => c.FamiliaProductosArrastre).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(c => c.Logs).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
