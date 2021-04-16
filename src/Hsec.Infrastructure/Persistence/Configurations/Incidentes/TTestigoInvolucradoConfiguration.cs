using Hsec.Domain.Entities.Incidentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Incidentes
{
    public class TTestigoInvolucradoConfiguration: IEntityTypeConfiguration<TTestigoInvolucrado>
    {
        public void Configure(EntityTypeBuilder<TTestigoInvolucrado> builder)
        {
            builder.HasKey(t => new { t.CodIncidente, t.Correlativo });
            builder
                .HasOne(t => t.CodIncidenteNavigation)
                .WithMany(t => t.TtestigoInvolucrado)
                .HasForeignKey(t => t.CodIncidente);
        }
    }
}
