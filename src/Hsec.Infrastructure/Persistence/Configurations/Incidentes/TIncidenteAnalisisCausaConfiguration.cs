using Hsec.Domain.Entities.Incidentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Incidentes
{
    public class TIncidenteAnalisisCausaConfiguration: IEntityTypeConfiguration<TIncidenteAnalisisCausa>
    {

        public void Configure(EntityTypeBuilder<TIncidenteAnalisisCausa> builder)
        {
            builder.HasKey(t => new { t.CodIncidente, t.CodAnalisis });
            builder
                .HasOne(t => t.CodIncidenteNavigation)
                .WithMany(t => t.TincidenteAnalisisCausa)
                .HasForeignKey(t => t.CodIncidente);
        }
    }
}
