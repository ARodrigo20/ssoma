using Hsec.Domain.Entities.Observaciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsec.Infrastructure.Persistence.Configurations.Observaciones
{
    public class TObervacionVerControlCriticoConfiguration : IEntityTypeConfiguration<TObservacionVerControlCritico>
    {
        public void Configure(EntityTypeBuilder<TObservacionVerControlCritico> builder)
        {
            builder.HasKey(t => t.CodVcc);

            builder.Property(t => t.CodVcc)
            .HasMaxLength(50);
            builder.Property(t => t.CodCartilla)
            .HasMaxLength(50);
            builder.Property(t => t.CodObservacion)
            .HasMaxLength(50);
            builder.Property(t => t.CodObservadoPor)
            .HasMaxLength(50);
            builder.Property(t => t.CodPosicionGer)
            .HasMaxLength(50);
            //builder.Property(t => t.TareaObservada)
            //.HasMaxLength(200);
            builder.Property(t => t.TareaObservada)
            .HasColumnType("nvarchar(max)");
            builder.Property(t => t.Empresa)
            .HasMaxLength(200);
        }
    }
}