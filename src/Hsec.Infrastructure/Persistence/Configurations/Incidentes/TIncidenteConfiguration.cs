using Hsec.Domain.Entities.Incidentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Incidentes
{
    public class TIncidenteConfiguration : IEntityTypeConfiguration<TIncidente>
    {
        public void Configure(EntityTypeBuilder<TIncidente> builder)
        {
            // builder.Property(t => t.Correlativo).UseIdentityColumn(1,1);
            builder.HasKey(t => t.CodIncidente);

            builder.Property(t => t.EstadoIncidente).HasMaxLength(1);

        }
    }
}