using Hsec.Domain.Entities.Inspecciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Inspecciones
{
    public class TDetalleInspeccionConfiguration : IEntityTypeConfiguration<TDetalleInspeccion>
    {
        public void Configure(EntityTypeBuilder<TDetalleInspeccion> builder)
        {
            builder.HasKey(k => k.Correlativo);
            builder.HasOne(o => o.Inspeccion)
                .WithMany(m => m.DetalleInspeccion)
                .HasForeignKey(f => f.CodInspeccion)
                .HasConstraintName("FK_1n_Inspeccion_Detalle");
        }
    }
}
