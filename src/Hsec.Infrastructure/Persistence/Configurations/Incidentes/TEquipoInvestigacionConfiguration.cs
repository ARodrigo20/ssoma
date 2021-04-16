using Hsec.Domain.Entities.Incidentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Incidentes
{
    public class TEquipoInvestigacionConfiguration: IEntityTypeConfiguration<TEquipoInvestigacion>
    {

        public void Configure(EntityTypeBuilder<TEquipoInvestigacion> builder)
        {
            builder.HasKey(t => new { t.Correlativo, t.CodIncidente });
            builder
                .HasOne(t => t.CodIncidenteNavigation)
                .WithMany(t => t.TequipoInvestigacion)
                .HasForeignKey(t => t.CodIncidente);
        }
    }
}
