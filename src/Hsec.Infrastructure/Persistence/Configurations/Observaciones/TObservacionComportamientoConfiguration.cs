using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations
{
    public class TObservacionComportamientoConfiguration : IEntityTypeConfiguration<TObservacionComportamiento>
    {
        public void Configure(EntityTypeBuilder<TObservacionComportamiento> builder)
        {
            //builder.Property(t => t.CodObservacion).ValueGeneratedNever();
            builder.HasKey(t => t.CodObservacion);
        }
    }
}
