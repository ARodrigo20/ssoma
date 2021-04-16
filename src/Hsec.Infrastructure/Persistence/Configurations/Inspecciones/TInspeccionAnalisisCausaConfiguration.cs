using Hsec.Domain.Entities.Inspecciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Inspecciones
{
    public class TInspeccionAnalisisCausaConfiguration : IEntityTypeConfiguration<TInspeccionAnalisisCausa>
    {
        public void Configure(EntityTypeBuilder<TInspeccionAnalisisCausa> builder)
        {
            builder.HasKey(k => k.Correlativo);
            builder.HasOne(o => o.Inspeccion)
                .WithMany(m => m.AnalisisCausa)
                .HasForeignKey(f => f.CodInspeccion)
                .HasConstraintName("FK_1n_Inspeccion_Analisis_Causa");
        }
    }
}