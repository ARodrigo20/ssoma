using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObsVCCCierreIteraccionConfiguration : IEntityTypeConfiguration<TObsVCCCierreIteraccion>
    {
        public void Configure(EntityTypeBuilder<TObsVCCCierreIteraccion> builder)
        {
            builder.HasKey(t => t.CodCierreIt);

            builder.Property(t => t.CodVcc)
            .HasMaxLength(50);
            builder.Property(t => t.CodDesCierreIter)
            .HasMaxLength(50);
            builder.Property(t => t.Respuesta)
            .HasMaxLength(50);
        }
    }
}