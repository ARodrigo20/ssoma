using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObsTaComentarioConfiguration : IEntityTypeConfiguration<TObsTaComentario>
    {
        public void Configure(EntityTypeBuilder<TObsTaComentario> builder)
        {
            builder.HasKey(t => new { t.CodObservacion, t.Orden });

        }
    }
}
