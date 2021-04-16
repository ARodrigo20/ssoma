using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObsVCCRespuestaConfiguration : IEntityTypeConfiguration<TObsVCCRespuesta>
    {
        public void Configure(EntityTypeBuilder<TObsVCCRespuesta> builder)
        {
            builder.HasKey(t => t.CodRespuesta);

            builder.Property(t => t.CodigoCriterio)
            .HasMaxLength(50);
            builder.Property(t => t.CodigoCC)
            .HasMaxLength(50);
            builder.Property(t => t.CodVcc)
            .HasMaxLength(50);
            builder.Property(t => t.Respuesta)
            .HasMaxLength(50);
        }
    }
}