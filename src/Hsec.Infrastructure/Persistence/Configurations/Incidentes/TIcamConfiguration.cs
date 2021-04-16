using Hsec.Domain.Entities.Incidentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Incidentes
{
    public class TIcamConfiguration : IEntityTypeConfiguration<TIcam>
    {
  
        public void Configure(EntityTypeBuilder<TIcam> builder)
        {
            builder.HasKey(t => new { t.Correlativo, t.CodIncidente });

            builder
                .HasOne(t => t.CodIncidenteNavigation)
                .WithMany(t => t.Ticam)
                .HasForeignKey(t => t.CodIncidente);
        }
    }
}
