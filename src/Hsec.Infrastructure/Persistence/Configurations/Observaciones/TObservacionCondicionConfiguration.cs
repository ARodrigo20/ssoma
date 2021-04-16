using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObservacionCondicionConfiguration : IEntityTypeConfiguration<TObservacionCondicion>
    {
        public void Configure(EntityTypeBuilder<TObservacionCondicion> builder)
        {
            //builder.Property(t => t.CodObservacion).ValueGeneratedNever();
            builder.HasKey(t => t.CodObservacion);

            //builder.HasOne<TObservacion>();
                
        }
    }
}
