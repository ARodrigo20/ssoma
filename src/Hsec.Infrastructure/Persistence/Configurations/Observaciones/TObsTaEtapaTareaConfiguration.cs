using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObsTaEtapaTareaConfiguration : IEntityTypeConfiguration<TObsTaEtapaTarea>
    {
        public void Configure(EntityTypeBuilder<TObsTaEtapaTarea> builder)
        {
            builder.HasKey(t => new { t.Correlativo, t.CodObservacion });
        }
    }
}
