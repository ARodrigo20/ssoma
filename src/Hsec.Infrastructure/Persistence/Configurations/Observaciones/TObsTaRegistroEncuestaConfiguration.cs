using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObsTaRegistroEncuestaConfiguration : IEntityTypeConfiguration<TObsTaRegistroEncuesta>
    {
        public void Configure(EntityTypeBuilder<TObsTaRegistroEncuesta> builder)
        {
            builder.HasKey(t => new { t.CodObservacion,t.CodPregunta});
        }
    }
}
