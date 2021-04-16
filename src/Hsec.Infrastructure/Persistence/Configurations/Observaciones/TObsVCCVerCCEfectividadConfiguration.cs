using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObsVCCVerCCEfectividadConfiguration : IEntityTypeConfiguration<TObsVCCVerCCEfectividad>
    {
        public void Configure(EntityTypeBuilder<TObsVCCVerCCEfectividad> builder)
        {
            builder.HasKey(t => t.Correlativo);

            builder.Property(t => t.CodVcc)
            .HasMaxLength(50);
            builder.Property(t => t.CodCartilla)
            .HasMaxLength(50);
            builder.Property(t => t.CodCC)
            .HasMaxLength(50);
            builder.Property(t => t.Efectividad)
            .HasMaxLength(200);
        }
    }
}