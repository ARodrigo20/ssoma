using Hsec.Domain.Entities.Incidentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Incidentes
{
    public class TAfectadoComunidadConfiguration : IEntityTypeConfiguration<TAfectadoComunidad>
    {

        public void Configure(EntityTypeBuilder<TAfectadoComunidad> builder)
        {
            builder.HasKey(t => new { t.Correlativo, t.CodIncidente });

            builder
                .HasOne(t => t.CodIncidenteNavigation)
                .WithMany(t => t.TafectadoComunidad)
                .HasForeignKey(t => t.CodIncidente);
        }
        //public virtual TMotivo CodMotivoNavigation { get; set; }
    }
}
