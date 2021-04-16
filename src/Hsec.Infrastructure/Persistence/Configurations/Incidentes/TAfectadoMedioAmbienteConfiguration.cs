using Hsec.Domain.Entities.Incidentes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Incidentes
{ 
    public class TAfectadoMedioAmbienteConfiguratuion: IEntityTypeConfiguration<TAfectadoMedioAmbiente>
    {

        public void Configure(EntityTypeBuilder<TAfectadoMedioAmbiente> builder)
        {
            builder.HasKey(t => new {  t.Correlativo, t.CodIncidente });
            builder
                .HasOne(t => t.CodIncidenteNavigation)
                .WithMany(t => t.TafectadoMedioAmbiente)
                .HasForeignKey(t => t.CodIncidente);
        }
    }
}
