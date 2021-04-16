using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObservacionConfiguration : IEntityTypeConfiguration<TObservacion>
    {
        public void Configure(EntityTypeBuilder<TObservacion> builder)
        {
            builder.HasKey(t => t.CodObservacion);

            //builder.HasMany<TObservacion>()
            //    .WithOne()
            //    .HasForeignKey(t => t.CodNivelRiesgo);
            //builder.HasOne<TObservacionComportamiento>()
            //    .WithOne(a => a.Observacion)
            //    .HasPrincipalKey<TObservacionComportamiento>(a=>a.CodObservacion);

            //builder.Property(t => t.CodObservacion)
            //    .IsUnicode(true);

        }
    }
}
