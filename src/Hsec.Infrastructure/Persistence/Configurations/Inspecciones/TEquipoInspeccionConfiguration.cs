using Hsec.Domain.Entities.Inspecciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Inspecciones
{
    public class TEquipoInspeccionConfiguration : IEntityTypeConfiguration<TEquipoInspeccion>
    {
        public void Configure(EntityTypeBuilder<TEquipoInspeccion> builder)
        {
            builder.HasKey(k => new { k.CodInspeccion, k.CodPersona })
                .HasName("PK_TInspeccion_EquipoInspeccion");
            builder.HasOne(o => o.Inspeccion)
                .WithMany(m => m.EquipoInspeccion)
                .HasForeignKey(f => f.CodInspeccion)
                .HasConstraintName("FK_1n_Inspeccion_EquipoInspeccion");
        }
    }
}
