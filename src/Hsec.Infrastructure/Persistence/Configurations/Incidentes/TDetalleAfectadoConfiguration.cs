using Hsec.Domain.Entities.Incidentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Incidentes
{
    public class TDetalleAfectadoConfiguration : IEntityTypeConfiguration<TDetalleAfectado>
    {

        public void Configure(EntityTypeBuilder<TDetalleAfectado> builder)
        {
            builder.HasKey(t => new { t.Correlativo, t.CodIncidente });
            builder
                .HasOne(t => t.CodIncidenteNavigation)
                .WithMany(t => t.TdetalleAfectado)
                .HasForeignKey(t => t.CodIncidente);
        }
    }
}
