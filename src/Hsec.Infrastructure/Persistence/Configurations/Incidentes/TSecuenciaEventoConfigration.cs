using Hsec.Domain.Entities.Incidentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Incidentes
{
    public class TSecuenciaEventoConfiguration: IEntityTypeConfiguration<TSecuenciaEvento>
    {

        public void Configure(EntityTypeBuilder<TSecuenciaEvento> builder)
        {
            builder.HasKey(t => new { t.CodIncidente, t.Correlativo });
            builder
                .HasOne(t => t.CodIncidenteNavigation)
                .WithMany(t => t.TsecuenciaEvento)
                .HasForeignKey(t => t.CodIncidente);
        }
    }
}
