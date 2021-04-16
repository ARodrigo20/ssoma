using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObsISRegistroEncuestaConfiguration : IEntityTypeConfiguration<TObsISRegistroEncuesta>
    {
        public void Configure(EntityTypeBuilder<TObsISRegistroEncuesta> builder)
        {
            builder.HasKey(t => new{t.CodObservacion,t.CodDescripcion,t.CodEncuesta});
        }
    }
}
