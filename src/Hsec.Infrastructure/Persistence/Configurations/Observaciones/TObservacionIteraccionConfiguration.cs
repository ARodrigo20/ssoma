using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObservacionIteraccionConfiguration : IEntityTypeConfiguration<TObservacionIteraccion>
    {
        public void Configure(EntityTypeBuilder<TObservacionIteraccion> builder)
        {
            //builder.Property(t => t.CodObservacion).ValueGeneratedNever();
            builder.HasKey(t => t.CodObservacion);

            //builder.HasOne<TObservacion>(t => t.Observacion)
            //    .WithOne(t => t.IteraccionSeguridad)
            //    .HasForeignKey<TObservacionIteraccion>(t => t.Correlativo);

        }
    }
}
