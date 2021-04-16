using Hsec.Domain.Entities.Incidentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Incidentes
{
    public class TAfectadoPropiedadConfiguration: IEntityTypeConfiguration<TAfectadoPropiedad>
    {

        public void Configure(EntityTypeBuilder<TAfectadoPropiedad> builder)
        {
            builder.HasKey(t => new { t.Correlativo, t.CodIncidente });
            builder
                .HasOne(t => t.CodIncidenteNavigation)
                .WithMany(t => t.TafectadoPropiedad)
                .HasForeignKey(t => t.CodIncidente);
        }
        //public virtual TtipoActivo CodTipActivoNavigation { get; set; }
    }
}
