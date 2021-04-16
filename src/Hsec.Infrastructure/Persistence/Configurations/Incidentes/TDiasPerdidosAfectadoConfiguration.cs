using Hsec.Domain.Entities.Incidentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Incidentes
{
    public class TDiasPerdidosAfectadoConfiguration : IEntityTypeConfiguration<TDiasPerdidosAfectado>
    {

        public void Configure(EntityTypeBuilder<TDiasPerdidosAfectado> builder)
        {
            builder.HasKey(t => new { t.Correlativo, t.CodIncidente, t.CodPersona });
            builder
                .HasOne(t => t.CodIncidenteNavigation)
                .WithMany(t => t.TdiasPerdidosAfectado)
                .HasForeignKey(t => t.CodIncidente);
        }
    }
}
