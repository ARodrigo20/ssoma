using Hsec.Domain.Entities.Incidentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Incidentes
{
    public class TInvestigaAfectadoConfiguration: IEntityTypeConfiguration<TInvestigaAfectado>
    {

        public void Configure(EntityTypeBuilder<TInvestigaAfectado> builder)
        {
            builder.HasKey(t => new { t.CodIncidente, t.Correlativo });
            builder
                .HasOne(t => t.CodIncidenteNavigation)
                .WithMany(t => t.TinvestigaAfectado)
                .HasForeignKey(t => t.CodIncidente);
        }
        //public virtual TtipoAfectado CodTipoAfectadoNavigation { get; set; }
        //public virtual TzonasDeLesion CodZonasDeLesionNavigation { get; set; }
    }
}
