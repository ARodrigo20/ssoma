using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObservacionTareaConfiguration : IEntityTypeConfiguration<TObservacionTarea>
    {
        public void Configure(EntityTypeBuilder<TObservacionTarea> builder)
        {
            //builder.Property(t => t.CodObservacion).ValueGeneratedNever();
            builder.HasKey(t => t.CodObservacion);
            // builder.Property(t => t.CodObservacion).

            //builder.HasOne<TObservacion>(t => t.Observacion)
            //    .WithOne(t => t.Tarea)
            //    .HasForeignKey<TObservacionIteraccion>(t => t.Correlativo);

        }
    }
}
