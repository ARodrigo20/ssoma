using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObsTaPersonaObservadaConfiguration : IEntityTypeConfiguration<TObsTaPersonaObservada>
    {
        public void Configure(EntityTypeBuilder<TObsTaPersonaObservada> builder)
        {
            builder.HasKey(t => new { t.CodObservacion, t.CodPersonaMiembro } );
        }
    }
}
