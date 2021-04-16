using Hsec.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.General
{
    public class TAprobacionPlanHistorialConfiguration : IEntityTypeConfiguration<TAprobacionPlanHistorial>
    {
        public void Configure(EntityTypeBuilder<TAprobacionPlanHistorial> builder)
        {
            builder.HasKey(t => t.Correlativo);
            builder.Property(f => f.Correlativo)
            .ValueGeneratedOnAdd();
            builder.Property(t => t.CodPersona)
                .HasMaxLength(30);
            builder.Property(t => t.EstadoAprobacion)
                .HasMaxLength(1);
        }
    }
}